using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class Package : IPackage
    {
        public IPackageMetadata Metadata { get; private set; }
        public ZipArchive Archive { get; private set; }

        bool disposed = false;

        public Package(IPackageMetadata metadata, ZipArchive archive) {
            this.Metadata = metadata;
            this.Archive = archive;
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
