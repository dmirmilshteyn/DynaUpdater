using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updater.Installation.Instructions;

namespace Updater
{
    public class Package : IPackage
    {
        public IPackageMetadata Metadata { get; private set; }
        public ZipArchive Archive { get; private set; }
        public IInstructionCollection Instructions { get; private set; }

        bool disposed = false;

        public Package(IPackageMetadata metadata, ZipArchive archive, IInstructionCollection instructions) {
            this.Metadata = metadata;
            this.Archive = archive;
            this.Instructions = instructions;
        }

        public void Dispose() {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    this.Archive.Dispose();
                }

                disposed = true;
            }
        }
    }
}
