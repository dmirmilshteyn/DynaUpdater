﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public interface IInstalledPackageMetadata : IPackageMetadata
    {
        DateTime InstallDate { get; }
    }
}
