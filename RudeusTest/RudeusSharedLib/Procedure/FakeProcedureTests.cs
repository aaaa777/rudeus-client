using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;

namespace RudeusSharedLibTest.RudeusSharedLib.Procedure
{
    public class FakeProcedureTests
    {
        [Fact]
        public void TestRun()
        {
            var fp = FakeProcedure.Create();
            
            fp.Run();
            fp.Run();

            Assert.Equal(2, fp.RunCount);
        }
    }
}
