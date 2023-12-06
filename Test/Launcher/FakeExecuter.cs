using Rudeus.Launcher.Procedure;
using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.SharedLib.Procedure;

namespace Test.Launcher
{
    public class FakeExecuter : FakeProcedure, IExecuter
    {
        public int ExitCode { get => 0; set { } }
    }
}
