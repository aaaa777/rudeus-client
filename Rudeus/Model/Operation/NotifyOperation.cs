using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Operation
{
    internal class NotifyOperation : Operation
    {
        public string Opcode = "Notify";
        
        public void Start()
        {
            Console.WriteLine($"{Opcode} executed");
        }
    }
}
