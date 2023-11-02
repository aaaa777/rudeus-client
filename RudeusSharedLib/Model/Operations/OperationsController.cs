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
    internal class OperationsController
    {
        private string Opcode = "undefined";
        private Func<string, bool> Start;
        private static bool IsInitializedDefault = false;
        public static List<OperationWrapper> Operations { get; set; } = new List<OperationWrapper>();

        public static void AddOperation(OperationWrapper operation)
        {
            Operations.Add(operation);
        }


        public static OperationWrapper? Run(string opcode, string? message="")
        {
            if(message == null)
            {
                message = string.Empty;
            }
            var operation = Search(opcode);
            operation?.Start(message);
            return operation;
        }
        public static OperationWrapper? Search(string opcode)
        {
            // opcodeが一致するものを検索
            foreach (OperationWrapper operation in Operations)
            {
                if (operation.Opcode == opcode)
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
