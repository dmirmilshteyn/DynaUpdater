using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Updater.Installation.Instructions
{
    public class InstructionCollection : List<IInstruction>, IInstructionCollection
    {
        public static InstructionCollection LoadFromXml(XmlReader xmlReader) {
            InstructionCollection instructionCollection = new InstructionCollection();

            while (xmlReader.Read()) {
                if (xmlReader.IsStartElement()) {
                    switch (xmlReader.Name) {
                        case "Extract": {
                                string inputPattern = xmlReader.GetAttribute("InputPattern");
                                string targetPattern = xmlReader.GetAttribute("TargetPattern");
                                ExtractInstruction extractInstruction = new ExtractInstruction(inputPattern, targetPattern);

                                instructionCollection.Add(extractInstruction);
                            }
                            break;
                    }
                }
            }

            return instructionCollection;
        }
    }
}
