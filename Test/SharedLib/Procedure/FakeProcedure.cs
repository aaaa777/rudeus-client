using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.SharedLib.Procedure
{
    /// <summary>
    /// IProcedureのFake実装
    /// </summary>
    public class FakeProcedure : IFakeProcedure
    {
        /// <summary>
        /// Runメソッドが呼ばれた回数
        /// </summary>
        public int RunCount { get; set; }

        // TODO: 自分自身を返すメソッドはインターフェースで定義できない問題の対応
        public FakeProcedure() { }

        /// <inheritdoc/>
        public async Task Run() { RunCount++; }

    }
}
