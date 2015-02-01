using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.PackageAssembler
{
    class Options
    {
        [Option('d', "id", Required = true, HelpText = "Sets the id of the generated package")]
        public int PackageId { get; set; }

        [Option('n', "name", Required = true, HelpText = "Sets the name of the generated package")]
        public string PackageName { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output package file")]
        public string OutputFile { get; set; }

        [Option('r', "repository", Required = true, HelpText = "The path to the existing repository file")]
        public string RepositoryFile { get; set; }

        [Option('i', "installation", Required = true, HelpText = "The installation file to be used in the package")]
        public string InstallationFile { get; set; }

        [Option('c', "contents", Required = true, HelpText = "The contents to include in the package")]
        public string ContentsSource { get; set; }

        [HelpOption]
        public string GetUsage() {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Updater Package Assembler");

            return stringBuilder.ToString();
        }
    }
}
