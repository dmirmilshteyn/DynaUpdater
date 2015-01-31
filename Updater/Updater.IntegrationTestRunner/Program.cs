using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.IntegrationTestRunner
{
    class Program
    {
        static void Main(string[] args) {
            string updateSite = ConfigurationManager.AppSettings["updateSite"];
        }
    }
}
