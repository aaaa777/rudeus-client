using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;

namespace RudeusSharedLibTest.Procedure
{
    public class FakeProcedureTests
    {
        [Fact]
        public async Task TestRun()
        {
            var fp = new FakeProcedure();
            
            await fp.Run();
            await fp.Run();

            Assert.Equal(2, fp.RunCount);
        }
    }
}
