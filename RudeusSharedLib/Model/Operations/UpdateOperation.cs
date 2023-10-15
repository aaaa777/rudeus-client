using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Operations
{
    internal class UpdateOperation
    {
        public string Opcode = "Update";

        public UpdateOperation()
        {
            new Operation(Opcode, Start);
        }

        public bool Start(string messasge)
        {
            Console.WriteLine($"{Opcode} executed");
            return true;
        }
    }
}
