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
        public string Opcode = "notify_toast";

        public NotifyOperation()
        {
            OperationWrapper opw = new OperationWrapper(Opcode, this.Start);
            OperationsController.AddOperation(opw);
        }

        public bool Start(string messasge)
        {
            Console.WriteLine($"{Opcode} executed");
            Notificator.Toast("情報センターからの通知", messasge);
            return true;
        }
    }
}
