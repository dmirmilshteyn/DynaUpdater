using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater
{
    public class UpdaterCache : IUpdaterCache
    {
        public IUpdaterCacheStorageProvider StorageProvider { get; private set; }

        public UpdaterCache(IUpdaterCacheStorageProvider storageProvider) {
            this.StorageProvider = storageProvider;
        }

        public IInstalledPackageMetadataCollection LoadInstalledMetadataCollection() {
            IInstalledPackageMetadataCollection installedMetadataCollection = new InstalledPackageMetadataCollection();

            using (XmlReader xmlReader = StorageProvider.GetInstalledPackageMetadataReader()) {
                while (xmlReader.Read()) {
                    if (xmlReader.IsStartElement()) {
                        switch (xmlReader.Name) {
                            case "Package": {
                                    // Package metadata is stored as attributes
                                    int id = Convert.ToInt32(xmlReader.GetAttribute("Id"));
                                    string name = xmlReader.GetAttribute("Name");
                                    string hash = xmlReader.GetAttribute("Hash");
                                    long size = Convert.ToInt64(xmlReader.GetAttribute("Size"));
                                    DateTime modifiedDate = DateTime.FromBinary(Convert.ToInt64(xmlReader.GetAttribute("ModifiedDate")));
                                    DateTime installDate = DateTime.FromBinary(Convert.ToInt64(xmlReader.GetAttribute("InstallDate")));

                                    IInstalledPackageMetadata packageMetadata = new InstalledPackageMetadata(id, name, hash, size, modifiedDate, installDate);
                                    installedMetadataCollection.Add(packageMetadata);
                                }
                                break;
                        }
                    }
                }
            }

            return installedMetadataCollection;
        }
    }
}
