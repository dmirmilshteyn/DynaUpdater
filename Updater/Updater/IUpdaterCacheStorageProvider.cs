using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater
{
    public interface IUpdaterCacheStorageProvider
    {
        /// <summary>
        /// Gets the installed package metadata reader. Reads from the file-system, using the platform implementation. 
        /// If the installed package metadata cache does not exist, it is created.
        /// </summary>
        /// <returns></returns>
        XmlReader GetInstalledPackageMetadataReader();
    }
}
