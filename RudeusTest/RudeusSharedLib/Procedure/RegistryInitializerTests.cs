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
            RegistryInitializer re = new RegistryInitializer();
            re.ConfSettings = FakeSettings.Create();
            re.InnoSettings = FakeSettings.Create();
            re.BgSettings = FakeSettings.Create();
            re.BfSettings = FakeSettings.Create();

            re.Run();
        }
    }
}
