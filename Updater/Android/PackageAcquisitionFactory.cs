using System;
using Updater.Storage;

namespace Updater.Android
{
	public class PackageAcquisitionFactory : IPackageAcquisitionFactory
	{
		public IPackageAcquisition BuildPackageAcquisition(Uri remotePackageStorageDirectory, ICacheStorageProvider storageProvider) {
			return new PackageAcquisition(remotePackageStorageDirectory, storageProvider);
		}
	}
}

