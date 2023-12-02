using Rudeus.Procedure;
using RudeusSharedLibTest.RudeusSharedLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudeusSharedLibTest.RudeusSharedLib.Procedure
{
    public class RegistryInitializerTests
    {
        [Fact]
        public void TestRun()
        {
            var re = new RegistryInitializer(
                cfs: FakeSettings.Create(),
                ins: FakeSettings.Create(),
                bgs: FakeSettings.Create(),
                bfs: FakeSettings.Create()
            );
            
            re.Run();
        }
    }
}
