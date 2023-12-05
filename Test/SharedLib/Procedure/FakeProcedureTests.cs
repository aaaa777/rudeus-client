using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;

namespace Test.Procedure
{
    public class FakeProcedureTests
    {
        [Fact]
        public async Task TestRun()
        {
            // Arrange
            var fp = new FakeProcedure();
            
            // Act
            await fp.Run();
            await fp.Run();

            // Assert
            Assert.Equal(2, fp.RunCount);
        }
    }
}
