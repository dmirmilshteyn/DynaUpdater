using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                                string name = metadataReader.GetAttribute("Name");
                                string hash = metadataReader.GetAttribute("Hash");
                                long size = Convert.ToInt64(metadataReader.GetAttribute("Size"));
                                DateTime modifiedDate = DateTime.FromBinary(Convert.ToInt64(metadataReader.GetAttribute("ModifiedDate")));

                                IPackageMetadata packageMetadata = new PackageMetadata(name, hash, size, modifiedDate);
                                metadataCollection.Add(packageMetadata);
                            }
                            break;
                    }
                }
            }

            return metadataCollection;
        }

        public IUpdateState DetermineUpdateState(XmlReader metadataReader) {
            throw new NotImplementedException();
        }
    }
}
