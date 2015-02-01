using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Installation;

namespace Updater
{
    public class Updater : IUpdater
    {
        public IPackageMetadataCollection ParseMetadataCollectionXml(XmlReader metadataReader) {
            IPackageMetadataCollection metadataCollection = new PackageMetadataCollection();

            while (metadataReader.Read()) {
                if (metadataReader.IsStartElement()) {
                    switch (metadataReader.Name) {
                        case "Package": {
                                // Package metadata is stored as attributes
                                int id = Convert.ToInt32(metadataReader.GetAttribute("Id"));
                                string name = metadataReader.GetAttribute("Name");
                                string hash = metadataReader.GetAttribute("Hash");
                                long size = Convert.ToInt64(metadataReader.GetAttribute("Size"));
                                DateTime publishDate = DateTime.FromBinary(Convert.ToInt64(metadataReader.GetAttribute("PublishDate")));

                                IPackageMetadata packageMetadata = new PackageMetadata(id, name, hash, size, publishDate);
                                metadataCollection.Add(id, packageMetadata);
                            }
                            break;
                    }
                }
            }

            return metadataCollection;
        }

        public IUpdateState DetermineUpdateState(IInstalledPackageMetadataCollection installedPackages, IPackageMetadataCollection remotePackages) {
            ISet<IPackageMetadata> outdatedPackages = new HashSet<IPackageMetadata>();

            foreach (IPackageMetadata packageMetadata in remotePackages.Values) {
                bool shouldUpdate = false;

                IInstalledPackageMetadata installedPackageMetadata = null;
                if (installedPackages.TryGetValue(packageMetadata.Id, out installedPackageMetadata)) {
                    // Check if the installed version is outdated
                    if (installedPackageMetadata.PublishDate < packageMetadata.PublishDate) {
                        // This package is outdated - update it!
                        shouldUpdate = true;
                    }
                } else {
                    // This package has not been installed
                    shouldUpdate = true;
                }

                if (shouldUpdate) {
                    outdatedPackages.Add(packageMetadata);
                }
            }

            return new UpdateState(outdatedPackages);
        }

        public IPackageInstaller CreateInstaller() {
            return new PackageInstaller();
        }
    }
}
