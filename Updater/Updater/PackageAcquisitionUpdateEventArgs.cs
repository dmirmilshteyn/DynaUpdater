using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class PackageAcquisitionUpdateEventArgs : EventArgs
    {
        public int AcquisitionPercent { get; private set; }

        public PackageAcquisitionUpdateEventArgs(int acquistionPercent) {
            this.AcquisitionPercent = acquistionPercent;
        }
    }
}
