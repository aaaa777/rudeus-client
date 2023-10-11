﻿using Rudeus.Model;
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
        public string Opcode = "Update";

        public AddCertOperation()
        {
            new Operation(Opcode, Start);
        }

        public bool Start()
        {

            Console.WriteLine($"{Opcode} executed");
            return true;
        }
    }
}
