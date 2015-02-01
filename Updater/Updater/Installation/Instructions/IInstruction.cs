using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Installation.Instructions
{
    public interface IInstruction
    {
        InstructionType Type { get; }

        void Execute();
    }
}
