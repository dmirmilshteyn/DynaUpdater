using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater.Storage
{
    public interface ICacheStorageProvider : IDisposable
    {
        XmlReader OpenInstalledPackageRepository();
        XmlWriter CreateInstalledPackageRepository();

        Stream CreateTemporaryFile(string fileName);
        void StoreTemporaryFile(string fileName, Stream inputStream);
    }
}
