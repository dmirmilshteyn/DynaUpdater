using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Updater
{
    public static class RegexHelper
    {
        public static string TranslateWildcards(string input) {
            return "^" + input.Replace("*", ".*") + "$";
        }
    }
}
