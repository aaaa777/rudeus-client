using Rudeus.Model;
using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Command.Test
{
    public class WorkerTest
    {
        [Fact(Skip = "RemoteAPIがスタブ不可なのでスキップ")]
        public async void TestRunAsync1()
        {
            // Arrange
            ISettings settings = new FakeSettings();
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
            ISettings settings = new FakeSettings();
            IFakeProcedure accessTokenValidator = new FakeProcedure();
            IFakeProcedure scheduledRegularExecuter = new FakeProcedure();
            IFakeProcedure userLoginExecuter = new FakeProcedure();
            string[] args = new string[] { "mode=login" };

            Worker worker = new Worker(logger: null, args: args, settings: settings, accessTokenValidator: accessTokenValidator, scheduledRegularExecuter: scheduledRegularExecuter, userLoginExecuter: userLoginExecuter);

            // Act
            await worker.RunAsync();

            // Assert
            Assert.Equal(1, accessTokenValidator.RunCount);
            Assert.Equal(0, scheduledRegularExecuter.RunCount);
            Assert.Equal(1, userLoginExecuter.RunCount);
        }
    }
}
