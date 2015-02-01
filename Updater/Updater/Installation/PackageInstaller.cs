using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Updater.Installation.Instructions;

namespace Updater.Installation
{
    public class PackageInstaller : IPackageInstaller
    {
        public void Install(IPackage package) {
            foreach (IInstruction instruction in package.Instructions) {
                instruction.Execute(package);
            }
        }
    }
}
