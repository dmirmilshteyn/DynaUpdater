using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Updater.Installation;

namespace Updater
{
    public interface IUpdater
    {
        IPackageMetadataCollection ParseMetadataCollectionXml(XmlReader metadataReader);
        IUpdateState DetermineUpdateState(IInstalledPackageMetadataCollection installedPackages, IPackageMetadataCollection remotePackages);
        IPackageInstaller CreateInstaller();
    }
}
