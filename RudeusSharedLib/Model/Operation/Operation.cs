using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudeusBg.Model.Operation
{
    /// <summary>
    /// サーバから受け取った命令コードと実行クラスを対応付けるクラス
    /// </summary>
    internal class Operation
    {
        public string Opcode = "";
        public static Operation[] Instanses { get; set; }

        public static Operation Search(string opcode)
        {
            foreach (var op in Instanses)
            {
                if (op.Equals(opcode))
                {
                    return op;
                }
            }
            return null;
        }

        public bool Equals(string opcode)
        {
            return opcode == Opcode;
        }

        public Operation()
        {
            Instanses.Prepend(this);
        }

    }
}
