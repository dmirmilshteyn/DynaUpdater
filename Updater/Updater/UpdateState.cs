using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class UpdateState : IUpdateState
    {
        public ISet<IPackageMetadata> Packages { get; private set; }

        public UpdateState(ISet<IPackageMetadata> packages) {
            this.Packages = packages;
        }


    }
}
