using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class PackageMetadataCollection : List<IPackageMetadata>, IPackageMetadataCollection
    {
    }
}
