using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class PackageMetadata : IPackageMetadata
    {
        public string Name { get; private set; }
        public string Hash { get; private set; }
        public long Size { get; private set; }
        public DateTime ModifiedTime { get; private set; }

        public PackageMetadata(string name, string hash, long size, DateTime modifiedTime) {
            this.Name = name;
            this.Hash = hash;
            this.Size = size;
            this.ModifiedTime = modifiedTime;
        }
    }
}
