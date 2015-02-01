﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class InstalledPackageMetadata : PackageMetadata, IInstalledPackageMetadata
    {
        public DateTime InstallDate { get; private set; }

        public InstalledPackageMetadata(int id, string name, string hash, long size, DateTime modifiedDate, DateTime installDate)
            : base(id, name, hash, size, modifiedDate) {
            this.InstallDate = installDate;
        }
    }
}
