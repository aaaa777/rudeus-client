using Rudeus.Procedure;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudeusSharedLibTest.Procedure
{
    public class RegistryInitializerTests
    {
        [Fact]
        public void TestRun()
        {
            var cfs = FakeSettings.Create();
            var ins = FakeSettings.Create();
            var bgs = FakeSettings.Create();
            var bfs = FakeSettings.Create();
            //var fakeLocalMachine = FakeLocalMachine.Create();

            var re = new RegistryInitializer(
                cfs: cfs,
                ins: ins,
                bgs: bgs,
                bfs: bfs
            );
            
            re.Run();


        }
    }
}
