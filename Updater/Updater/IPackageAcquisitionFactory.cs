using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updater.Storage;

namespace Updater
{
    public interface IPackageAcquisitionFactory
    {
        IPackageAcquisition BuildPackageAcquisition(Uri remotePackageStorageDirectory, ICacheStorageProvider storageProvider);
    }
}
