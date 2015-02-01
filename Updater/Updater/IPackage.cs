using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public interface IPackage : IDisposable
    {
        IPackageMetadata Metadata { get; }
        ZipArchive Archive { get; }
    }
}
