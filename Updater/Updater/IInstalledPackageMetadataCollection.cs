﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater
{
    public interface IInstalledPackageMetadataCollection : IDictionary<int, IInstalledPackageMetadata>
    {
        void Save(XmlWriter xmlWriter);
    }
}
