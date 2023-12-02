using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{
    internal class FakeProcedure : IProcedure
    {
        private int _runCount = 0;
        public int RunCount { get { return _runCount; } }

        public static FakeProcedure Create() { return new FakeProcedure(); }

        public void Run() { _runCount++; }

    }
}
