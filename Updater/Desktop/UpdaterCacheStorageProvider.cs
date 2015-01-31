using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater.Desktop
{
    public class UpdaterCacheStorageProvider : IUpdaterCacheStorageProvider
    {
        private readonly string InstalledPackageFile = "InstalledPackages.xml";

        public string CacheDirectory { get; private set; }

        public UpdaterCacheStorageProvider(string cacheDirectory) {
            this.CacheDirectory = cacheDirectory;
        }

        public XmlReader GetInstalledPackageMetadataReader() {
            string path = Path.Combine(CacheDirectory, InstalledPackageFile);

            if (File.Exists(path) == false) {
                CreateEmptyInstalledPackageMetadataFile(path);
            }

            return XmlReader.Create(path);
        }

        private void CreateEmptyInstalledPackageMetadataFile(string path) {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings)) {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Packages");

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }
    }
}
