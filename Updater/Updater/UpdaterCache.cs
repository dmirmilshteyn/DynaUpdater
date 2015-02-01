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
            using (XmlReader xmlReader = cacheStorageProvider.OpenInstalledPackageRepository()) {
                installedPackages = InstalledPackageMetadataCollection.LoadFromXml(xmlReader);
            }

            return new UpdaterCache(cacheStorageProvider, installedPackages);
        }

        public void MarkPackageAsInstalled(IPackageMetadata packageMetadata) {
            if (InstalledPackages.ContainsKey(packageMetadata.Id)) {
                // The package has previously been intalled - remove the data from the collection
                // The entry will be recreated later on with the updated value
                InstalledPackages.Remove(packageMetadata.Id);

            }

            IInstalledPackageMetadata installedMetadata = new InstalledPackageMetadata(packageMetadata.Id, packageMetadata.Name, packageMetadata.Hash, packageMetadata.Size, packageMetadata.PublishDate, DateTime.UtcNow);
            InstalledPackages.Add(installedMetadata.Id, installedMetadata);

            using (XmlWriter xmlWriter = StorageProvider.CreateInstalledPackageRepository()) {
                InstalledPackages.Save(xmlWriter);
            }
        }
    }
}
