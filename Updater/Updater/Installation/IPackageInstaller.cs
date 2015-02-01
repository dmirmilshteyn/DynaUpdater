using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updater.Storage;

namespace Updater.Installation
{
    public interface IPackageInstaller
    {
        void Install(IStorageProvider storageProvider, IPackage package);
    }
}
