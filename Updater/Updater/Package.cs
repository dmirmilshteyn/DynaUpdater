using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Installation.Instructions;

namespace Updater
{
    public class Package : IPackage
    {
        private readonly string[] IgnoredFiles = { "Installation.xml" };

        public IPackageMetadata Metadata { get; private set; }
        public IInstructionCollection Instructions { get; private set; }

        public IEnumerable<ZipArchiveEntry> Entries {
            get {
                foreach (ZipArchiveEntry entry in this.archive.Entries) {
                    if (IgnoredFiles.Contains(entry.FullName) == false) {
                        yield return entry;
                    }
                }
            }
        }

        bool disposed = false;
        ZipArchive archive;

        private Package(IPackageMetadata metadata, ZipArchive archive, IInstructionCollection instructions) {
            this.Metadata = metadata;
            this.Instructions = instructions;

            this.archive = archive;
        }

        public static Package OpenPackage(IPackageMetadata metadata, ZipArchive archive) {
            ZipArchiveEntry installationInstructionsEntry = archive.Entries.First((archiveEntry) => { return (archiveEntry.FullName == "Installation.xml"); });
            using (Stream installationInstructionsStream = installationInstructionsEntry.Open()) {
                using (XmlReader installationInstructionsReader = XmlReader.Create(installationInstructionsStream)) {
                    IInstructionCollection instructionCollection = InstructionCollection.LoadFromXml(installationInstructionsReader);

                    return new Package(metadata, archive, instructionCollection);
                }
            }
        }

        public void Dispose() {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    this.archive.Dispose();
                }

                disposed = true;
            }
        }
    }
}
