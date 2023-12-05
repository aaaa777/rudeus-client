using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Procedure
{
    /// <summary>
    /// IProcedureのFake実装
    /// </summary>
    public class FakeProcedure : IFakeProcedure
    {
        private int _runCount = 0;
        /// <summary>
        /// Runメソッドが呼ばれた回数
        /// </summary>
        public int RunCount { get { return _runCount; } }

        // TODO: 自分自身を返すメソッドはインターフェースで定義できない問題の対応
        public FakeProcedure() { }

        /// <inheritdoc/>
        public async Task Run() { _runCount++; }

    }
}
