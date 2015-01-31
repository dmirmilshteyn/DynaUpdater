using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater
{
    public interface IPackageFactory
    {
        IPackage CreatePackage(XmlReader metadataReader);
    }
}
