﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public interface IUpdateInstaller
    {
        void Install(IPackageMetadata packageMetadata);
    }
}