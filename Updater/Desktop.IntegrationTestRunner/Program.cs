using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Desktop;
using Updater.Installation;
using Updater.Installation.Instructions;
using Updater.Storage;

namespace Updater.IntegrationTestRunner
{
    class Program
    {
        static void Main(string[] args) {
            Task mainAsyncTask = MainAsync(args);
            mainAsyncTask.Wait();
        }

        static async Task MainAsync(string[] args) {
            Uri updateSite = new Uri(ConfigurationManager.AppSettings["updateSite"]);
            Uri remotePackageStorageDirectory = new Uri(updateSite, "Packages/");

            string baseDirectory = Path.GetFullPath("Testing");

            // Clean the old test environment if it already exist
            if (Directory.Exists(baseDirectory)) {
                Directory.Delete(baseDirectory, true);
            }
            // Setup the test environment
            Directory.CreateDirectory(baseDirectory);

            IStorageProvider storageProvider = new StorageProvider(baseDirectory);
            using (ICacheStorageProvider cacheStorageProvider = new CacheStorageProvider(Path.Combine(baseDirectory, "Cache"))) {
                IPackageAcquisitionFactory packageAcquisitionFactory = new PackageAcquisitionFactory();
                IUpdaterCache updaterCache = new UpdaterCache(cacheStorageProvider);

                IInstalledPackageMetadataCollection installedPackageMetadataCollection = updaterCache.LoadInstalledMetadataCollection();
                IPackageMetadataCollection packageMetadataCollection = null;

                IUpdater updater = new Updater();
                using (XmlReader xmlReader = XmlReader.Create(updateSite.AbsoluteUri)) {
                    packageMetadataCollection = updater.ParseMetadataCollectionXml(xmlReader);
                }

                IUpdateState updateState = updater.DetermineUpdateState(installedPackageMetadataCollection, packageMetadataCollection);
                IPackageInstaller packageInstaller = updater.CreateInstaller();
                foreach (IPackageMetadata packageMetadata in updateState.Packages) {
                    IPackageAcquisition packageAcquisition = packageAcquisitionFactory.BuildPackageAcquisition(remotePackageStorageDirectory, cacheStorageProvider);
                    using (ZipArchive packageArchive = await packageAcquisition.AcquirePackageArchive(packageMetadata)) {
                        using (IPackage package = Package.OpenPackage(packageMetadata, packageArchive)) {
                            packageInstaller.Install(storageProvider, package);
                        }
                    }
                }
            }
        }
    }
}
