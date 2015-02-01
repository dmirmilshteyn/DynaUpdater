using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Storage;

namespace Updater
{
    public class InstalledPackageMetadataCollection : Dictionary<int, IInstalledPackageMetadata>, IInstalledPackageMetadataCollection
    {
        public static InstalledPackageMetadataCollection LoadFromXml(XmlReader xmlReader) {
            InstalledPackageMetadataCollection installedMetadataCollection = new InstalledPackageMetadataCollection();

            while (xmlReader.Read()) {
                if (xmlReader.IsStartElement()) {
                    switch (xmlReader.Name) {
                        case "Package": {
                                // Package metadata is stored as attributes
                                int id = Convert.ToInt32(xmlReader.GetAttribute("Id"));
                                string name = xmlReader.GetAttribute("Name");
                                string hash = xmlReader.GetAttribute("Hash");
                                long size = Convert.ToInt64(xmlReader.GetAttribute("Size"));
                                DateTime publishDate = DateTime.FromBinary(Convert.ToInt64(xmlReader.GetAttribute("PublishDate")));
                                DateTime installDate = DateTime.FromBinary(Convert.ToInt64(xmlReader.GetAttribute("InstallDate")));

                                IInstalledPackageMetadata packageMetadata = new InstalledPackageMetadata(id, name, hash, size, publishDate, installDate);
                                installedMetadataCollection.Add(id, packageMetadata);
                            }
                            break;
                    }
                }
            }

            return installedMetadataCollection;
        }
    }
}
