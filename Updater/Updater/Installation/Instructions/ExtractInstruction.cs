using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Updater.Installation.Instructions
{
    public class ExtractInstruction : IInstruction
    {
        public InstructionType Type {
            get { return InstructionType.Extract; }
        }

        public string InputPattern { get; private set; }
        public string TargetPattern { get; private set; }

        Regex inputRegex;

        public ExtractInstruction(string inputPattern, string targetPattern) {
            this.InputPattern = inputPattern;
            this.TargetPattern = targetPattern;

            this.inputRegex = new Regex(RegexHelper.TranslateWildcards(this.InputPattern), RegexOptions.IgnoreCase);
        }

        public void Execute(IPackage package) {
            foreach (ZipArchiveEntry entry in package.Archive.Entries) {
                if (inputRegex.IsMatch(entry.FullName)) {
                    string destinationPath = this.TargetPattern.Replace("$(FileName)", entry.Name);
                }
            }
        }
    }
}
