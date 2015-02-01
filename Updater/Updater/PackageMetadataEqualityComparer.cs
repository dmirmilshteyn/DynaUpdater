using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class PackageMetadataEqualityComparer : IEqualityComparer<IPackageMetadata>
    {
        public bool Equals(IPackageMetadata x, IPackageMetadata y) {
            return (x.Id == y.Id);
        }

        public int GetHashCode(IPackageMetadata obj) {
            return obj.Id.GetHashCode();
        }
    }
}
