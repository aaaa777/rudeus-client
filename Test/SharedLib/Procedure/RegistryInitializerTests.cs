using Rudeus.Procedure;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.SharedLib.Model.Settings;

namespace Test.SharedLib.Procedure
{
    public class RegistryInitializerTests
    {
        [Fact]
        public async void TestRun()
        {
            // Arrange
            var cfs = new FakeSettings();
            var ins = new FakeSettings();
            var bgs = new FakeSettings();
            var bfs = new FakeSettings();
            //var fakeLocalMachine = FakeLocalMachine.Create();

            // Act
            var re = new RegistryInitializer(
                cfs: cfs,
                ins: ins,
                bgs: bgs,
                bfs: bfs
            );
            
            // Assert
            await re.Run();
        }
    }
}
