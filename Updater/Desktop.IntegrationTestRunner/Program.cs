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
using Updater.Installation.Instructions;

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

            using (IUpdaterCacheStorageProvider storageProvider = new UpdaterCacheStorageProvider(Path.Combine(baseDirectory, "Cache"))) {
                IUpdaterCache updaterCache = new UpdaterCache(storageProvider);

                IInstalledPackageMetadataCollection installedPackageMetadataCollection = updaterCache.LoadInstalledMetadataCollection();
                IPackageMetadataCollection packageMetadataCollection = null;

                IUpdater updater = new Updater();
                using (XmlReader xmlReader = XmlReader.Create(updateSite.AbsoluteUri)) {
                    packageMetadataCollection = updater.ParseMetadataCollectionXml(xmlReader);
                }

                IUpdateState updateState = updater.DetermineUpdateState(installedPackageMetadataCollection, packageMetadataCollection);
                IUpdateInstaller updateInstaller = updater.CreateInstaller();
                foreach (IPackageMetadata packageMetadata in updateState.Packages) {
                    IPackageAcquisition packageAcquisition = new PackageAcquisition(remotePackageStorageDirectory, storageProvider);
                    using (ZipArchive packageArchive = await packageAcquisition.AcquirePackageArchive(packageMetadata)) {
                        ZipArchiveEntry installationInstructionsEntry = packageArchive.Entries.First((archiveEntry) => { return (archiveEntry.FullName == "Installation.xml"); });
                        using (Stream installationInstructionsStream = installationInstructionsEntry.Open()) {
                            using (XmlReader installationInstructionsReader = XmlReader.Create(installationInstructionsStream)) {
                                IInstructionCollection instructionCollection = InstructionCollection.LoadFromXml(installationInstructionsReader);
                                using (IPackage package = new Package(packageMetadata, packageArchive, instructionCollection)) {

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
