using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ExtractInstruction(string inputPattern, string targetPattern) {
            this.InputPattern = inputPattern;
            this.TargetPattern = targetPattern;
        }

        public void Execute() {
        }
    }
}
