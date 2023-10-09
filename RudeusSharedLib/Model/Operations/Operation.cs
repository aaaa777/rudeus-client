using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Rudeus.Model.Operations
{
    /// <summary>
    /// サーバから受け取った命令コードと実行クラスを対応付けるクラス
    /// </summary>
    internal class Operation
    {
        private string Opcode;
        private Func<string> Callback;
        public static Operation[] Instanses { get; set; } = new Operation[0];

        public static Operation Run(string opcode)
        {
            // opcodeが一致するものを検索
            foreach (Operation op in Instanses)
            {
                if (op.Equals(opcode))
                {
                    // Callbackを呼び出して実行したOperationを返却
                    op.Callback();
                    return op;
                }
            }

            // opcodeが存在していなかったときnullを返して終了
            return null;
        }

        public static void InitializeOperations()
        {
            new AddCertOperation();
            new AddVPNOperation();
            new NotifyOperation();
            new UpdateOperation();
        }

        public bool Equals(string opcode)
        {
            return opcode == Opcode;
        }

        public Operation(string opcode, Func<string> callback)
        {
            Opcode = opcode;
            Callback = callback;
            
            Instanses.Prepend(this);
        }

    }
}
