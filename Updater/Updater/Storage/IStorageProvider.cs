using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Storage
{
    public interface IStorageProvider
    {
        Stream CreateFile(string relativePath);
        Stream OpenFile(string relativePath);

        void DeleteFile(string relativePath);
        void DeleteDirectory(string relativePath);
    }
}
