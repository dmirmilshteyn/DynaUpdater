using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class UpdateState : IUpdateState
    {
        public IPackageMetadataCollection Packages { get; private set; }

        public UpdateState(IPackageMetadataCollection packages) {
            this.Packages = packages;
        }


    }
}
