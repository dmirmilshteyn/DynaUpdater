using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updater.Storage;

namespace Updater.Desktop
{
    public class PackageAcquisitionFactory : IPackageAcquisitionFactory
    {
        public IPackageAcquisition BuildPackageAcquisition(Uri remotePackageStorageDirectory, ICacheStorageProvider storageProvider) {
            return new PackageAcquisition(remotePackageStorageDirectory, storageProvider);
        }
    }
}
