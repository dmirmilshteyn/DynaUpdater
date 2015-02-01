using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.PackageAssembler
{
    public static class Program
    {
        public static void Main(string[] args) {
            Options options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options)) {
                Console.WriteLine("Package Id: " + options.PackageId);
                Console.WriteLine("Package Name: " + options.PackageName);
                Console.WriteLine("Output File: " + options.OutputFile);
                Console.WriteLine("Repository File: " + options.RepositoryFile);
                Console.WriteLine("Installation File: " + options.InstallationFile);
                Console.WriteLine("Contents Directory: " + options.ContentsSource);
                Console.WriteLine();
            }
        }
    }
}
