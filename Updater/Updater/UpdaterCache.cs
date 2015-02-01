using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Storage;

namespace Updater
{
    public class UpdaterCache : IUpdaterCache
    {
        public ICacheStorageProvider StorageProvider { get; private set; }
        public IInstalledPackageMetadataCollection InstalledPackages { get; private set; }

        private UpdaterCache(ICacheStorageProvider storageProvider, IInstalledPackageMetadataCollection installedMetadataCollection) {
            this.StorageProvider = storageProvider;
            this.InstalledPackages = installedMetadataCollection;
        }

        public static UpdaterCache InitializeCache(ICacheStorageProvider cacheStorageProvider) {
            IInstalledPackageMetadataCollection installedPackages;
            using (XmlReader xmlReader = cacheStorageProvider.GetInstalledPackageMetadataReader()) {
                installedPackages = InstalledPackageMetadataCollection.LoadFromXml(xmlReader);
            }

            return new UpdaterCache(cacheStorageProvider, installedPackages);
        }


    }
}
