using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public static class UpdateStateExtensions
    {
        public static bool HasUpdate(this IUpdateState updateState) {
            return (updateState.Packages.Count > 0);
        }
    }
}
