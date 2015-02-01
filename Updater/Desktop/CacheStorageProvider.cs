using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Storage;

namespace Updater.Desktop
{
    public class CacheStorageProvider : ICacheStorageProvider
    {
        private readonly string InstalledPackageFile = "InstalledPackages.xml";

        public string CacheDirectory { get; private set; }
        public string TemporaryDirectory { get; private set; }

        bool disposed = false;

        public CacheStorageProvider(string cacheDirectory) {
            this.CacheDirectory = cacheDirectory;

            if (Directory.Exists(this.CacheDirectory) == false) {
                Directory.CreateDirectory(this.CacheDirectory);
            }

            // Create the temporary directory
            this.TemporaryDirectory = Path.Combine(this.CacheDirectory, "Temp");
            if (Directory.Exists(this.TemporaryDirectory) == false) {
                Directory.CreateDirectory(this.TemporaryDirectory);
            }
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

        public Stream CreateTemporaryFile(string fileName) {
            return new FileStream(Path.Combine(this.TemporaryDirectory, fileName), FileMode.CreateNew);
        }

        public void StoreTemporaryFile(string fileName, Stream inputStream) {
            using (Stream fileStream = CreateTemporaryFile(fileName)) {
                inputStream.CopyTo(fileStream);
            }
        }

        public void RemoveTemporaryFiles() {
            if (Directory.Exists(this.TemporaryDirectory)) {
                Directory.Delete(this.TemporaryDirectory, true);
            }
        }

        public void Dispose() {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    RemoveTemporaryFiles();
                }

                disposed = true;
            }
        }
    }
}
