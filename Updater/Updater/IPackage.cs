using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public interface IPackage
    {
        string Name { get; }
        DateTime Date { get; }
        IReadOnlyCollection<IPackageObject> Objects { get; }
    }
}
