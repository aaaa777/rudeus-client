using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Operations
{
    internal class AddCertOperation
    {
        public string Opcode = "cert_add";

        public AddCertOperation()
        {
            OperationWrapper opw = new OperationWrapper(Opcode, this.Start);
            OperationsController.AddOperation(opw);
        }

        public bool Start(string messasge)
        {

            Console.WriteLine($"{Opcode} executed");
            return true;
        }
    }
}
