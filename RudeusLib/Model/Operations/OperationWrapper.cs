using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model.Operations
{
    internal class OperationWrapper
    {
        public string Opcode = "undefined";
        public Func<string, bool> Callback;
        public OperationWrapper(string opcode, Func<string, bool> callback) 
        { 
            Opcode = opcode;
            Callback = callback;
        }

        public void Start(string message)
        {
            // Todo: bool捨ててる
            Callback(message);
        }
    }
}
