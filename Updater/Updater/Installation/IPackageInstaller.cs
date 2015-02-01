using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Installation
{
    public interface IPackageInstaller
    {
        void Install(IPackage package);
    }
}
