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
        private readonly string Opcode = "undefined";
        private readonly Func<bool> Start;
        private static bool IsInitializedDefault = false;
        public static Operation[] Operations { get; set; } = Array.Empty<Operation>();

        public Operation(string opcode, Func<bool> callback)
        {
            Opcode = opcode;
            Start = callback;

            _ = Operations.Prepend(this);
        }


        public static Operation? Run(string opcode)
        {
            var operation = Search(opcode);
            operation?.Start();
            return operation;
        }
        public static Operation? Search(string opcode) {
            // opcodeが一致するものを検索
            foreach (Operation operation in Operations)
            {
                if (operation.Equals(opcode))
                {
                    // Operationを返却
                    return operation;
                }
            }

            // opcodeが存在していなかったときnullを返して終了
            return null;
        }

        public static void InitializeDefaultOperations()
        {
            if(IsInitializedDefault) { return; }
            IsInitializedDefault = true;

            _ = new AddCertOperation();
            _ = new AddVPNOperation();
            _ = new NotifyOperation();
            _ = new UpdateOperation();
        }

        public bool Equals(string opcode)
        {
            return opcode == Opcode;
        }

    }
}
