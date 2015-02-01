using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updater.Installation.Instructions;

namespace Updater
{
    public interface IPackage : IDisposable
    {
        IPackageMetadata Metadata { get; }
        IInstructionCollection Instructions { get; }

        IEnumerable<ZipArchiveEntry> Entries { get; }
    }
}
