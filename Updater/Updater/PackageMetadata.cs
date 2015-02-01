using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class PackageMetadata : IPackageMetadata
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Hash { get; private set; }
        public long Size { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Source { get; private set; }

        public PackageMetadata(int id, string name, string hash, long size, DateTime modifiedDate, string source) {
            this.Id = id;
            this.Name = name;
            this.Hash = hash;
            this.Size = size;
            this.PublishDate = modifiedDate;
            this.Source = source;
        }
    }
}
