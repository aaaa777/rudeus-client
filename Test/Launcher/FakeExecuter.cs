using Microsoft.VisualBasic;
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
        public int ExitCode { get; set; }
        public bool ExitWithUpdateOnce { get; set; } = false;
        public async Task RunExe()
        {
            RunCount++;
            if(ExitWithUpdateOnce && RunCount == 1)
            {
                ExitCode = Rudeus.Constants.ForceUpdateExitCode;
                return;
            }
            ExitCode = 0;
        }
    }
}
