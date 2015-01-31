using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class UpdaterCache : IUpdaterCache
    {
        public IUpdaterCacheStorageProvider StorageProvider { get; private set; }

        public UpdaterCache(IUpdaterCacheStorageProvider storageProvider) {
            this.StorageProvider = storageProvider;
        }

        public IPackageMetadataCollection LoadMetadataCollection() {
            throw new NotImplementedException();
        }
    }
}
