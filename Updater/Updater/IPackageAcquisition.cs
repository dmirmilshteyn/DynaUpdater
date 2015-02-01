using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public interface IPackageAcquisition
    {
        event EventHandler<PackageAcquisitionUpdateEventArgs> AcquisitionUpdate;

        Task<ZipArchive> AcquirePackageArchive(IPackageMetadata packageMetadata);
    }
}
