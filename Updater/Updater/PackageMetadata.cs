using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class PackageMetadata : IPackageMetadata
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Hash { get; private set; }
        public long Size { get; private set; }
        public DateTime ModifiedDate { get; private set; }

        public PackageMetadata(int id, string name, string hash, long size, DateTime modifiedDate) {
            this.Id = id;
            this.Name = name;
            this.Hash = hash;
            this.Size = size;
            this.ModifiedDate = modifiedDate;
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }

            PackageMetadata comparisonMetadata = obj as PackageMetadata;
            if (comparisonMetadata == null) {
                return false;
            }

            return comparisonMetadata.Id == Id;
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
