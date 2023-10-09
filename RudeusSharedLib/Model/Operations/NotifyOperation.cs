using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Operations
{
    internal class NotifyOperation
    {
        public string Opcode = "Update";

        public NotifyOperation()
        {
            new Operation(Opcode, Start);
        }

        public string Start()
        {
            Console.WriteLine($"{Opcode} executed");
            return null;
        }
    }
}
