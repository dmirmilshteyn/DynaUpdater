using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class UpdateState : IUpdateState
    {
        public IInstalledPackageMetadataCollection InstalledPackages { get; private set; }

        public UpdateState(IInstalledPackageMetadataCollection installedPackages) {
            this.InstalledPackages = installedPackages;
        }
    }
}
