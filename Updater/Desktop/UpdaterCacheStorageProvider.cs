﻿using System;
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

        public string TemporaryDirectory { get; private set; }

        public UpdaterCacheStorageProvider(string cacheDirectory) {
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

        public void StoreTemporaryFile(string fileName, Stream inputStream) {
            using (FileStream fileStream = new FileStream(Path.Combine(this.TemporaryDirectory, fileName), FileMode.CreateNew)) {
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
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                RemoveTemporaryFiles();
            }
        }
    }
}
