using Rudeus.Command;
using Rudeus.Procedure;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Procedure;
using Test.SharedLib.Model.Settings;

namespace Test.Command
{
    public class WorkerTest
    {
        [Fact(Skip = "RemoteAPIがスタブ不可なのでスキップ")]
        public async void TestRunAsync1()
        {
            // Arrange
            IRootSettings settings = new FakeSettings();
            IFakeProcedure accessTokenValidator = new FakeProcedure();
            IFakeProcedure scheduledRegularExecuter = new FakeProcedure();
            IFakeProcedure userLoginExecuter = new FakeProcedure();
            string[] args = new string[] { };

            Worker worker = new Worker(logger: null, args: args, settings: settings, accessTokenValidator: accessTokenValidator, scheduledRegularExecuter: scheduledRegularExecuter, userLoginExecuter: userLoginExecuter);

            // Act
            await worker.RunAsync();

            // Assert
            Assert.Equal(1, accessTokenValidator.RunCount);
            Assert.Equal(1, scheduledRegularExecuter.RunCount);
            Assert.Equal(0, userLoginExecuter.RunCount);
        }

        [Fact(Skip = "RemoteAPIがスタブ不可なのでスキップ")]
        public async void TestRunAsync2()
        {
            // Arrange
            IRootSettings settings = new FakeSettings();
            IFakeProcedure accessTokenValidator = new FakeProcedure();
            IFakeProcedure scheduledRegularExecuter = new FakeProcedure();
            IFakeProcedure userLoginExecuter = new FakeProcedure();
            string[] args = new string[] { "mode=login" };

            var worker = new Worker(logger: null, args: args, settings: settings, accessTokenValidator: accessTokenValidator, scheduledRegularExecuter: scheduledRegularExecuter, userLoginExecuter: userLoginExecuter);

            // Act
            await worker.RunAsync();

            // Assert
            Assert.Equal(1, accessTokenValidator.RunCount);
            Assert.Equal(0, scheduledRegularExecuter.RunCount);
            Assert.Equal(1, userLoginExecuter.RunCount);
        }
    }
}
