using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Desktop;

namespace Updater.IntegrationTestRunner
{
    class Program
    {
        static void Main(string[] args) {
            string updateSite = ConfigurationManager.AppSettings["updateSite"];

            string baseDirectory = Path.GetFullPath("Testing");

            // Clean the old test environment if it already exist
            if (Directory.Exists(baseDirectory)) {
                Directory.Delete(baseDirectory, true);
            }
            // Setup the test environment
            Directory.CreateDirectory(baseDirectory);

            IUpdaterCacheStorageProvider storageProvider = new UpdaterCacheStorageProvider(Path.Combine(baseDirectory, "Cache"));
            IUpdaterCache updaterCache = new UpdaterCache(storageProvider);

            IInstalledPackageMetadataCollection installedPackageMetadataCollection = updaterCache.LoadInstalledMetadataCollection();
            IPackageMetadataCollection packageMetadataCollection = null;

            IUpdater updater = new Updater();
            using (XmlReader xmlReader = XmlReader.Create(updateSite)) {
                packageMetadataCollection = updater.ParseMetadataCollectionXml(xmlReader);
            }


        }
    }
}
