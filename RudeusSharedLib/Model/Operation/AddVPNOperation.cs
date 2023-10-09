using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RudeusBg.Model.Operation
{
    internal class AddVPNOperation : Operation
    {
        public string Opcode = "AddVPN";

        public void Start()
        {
            //CertificateAPI;
            Console.WriteLine($"{Opcode} executed");
        }
    }
}
