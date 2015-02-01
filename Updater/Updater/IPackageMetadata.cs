using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public interface IPackageMetadata
    {
        int Id { get; }
        string Name { get; }
        string Hash { get; }
        long Size { get; }
        DateTime PublishDate { get; }
    }
}
