using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater
{
    public interface IUpdater
    {
        IPackageMetadataCollection ParseMetadataCollectionXml(XmlReader metadataReader);
        IUpdateState DetermineUpdateState(XmlReader metadataReader);
    }
}
