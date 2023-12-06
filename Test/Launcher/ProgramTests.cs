using Rudeus.Launcher;
using SharedLib.Model.Settings;
using Test.SharedLib.Model.Settings;
using Test.SharedLib.Procedure;

namespace Test.Launcher
{
    public class ProgramTests
    {
        [Fact]
        public async Task TestMain()
        {
            // Arrange
            IAppSettings aps = new FakeSettings();
            IRootSettings rts = new FakeSettings();
            var fp = new FakeProcedure();
            var fe = new FakeExecuter();
            Program.AppSettings = aps;
            Program.RootSettings = rts;
            Program._updater = fp;
            Program._launcher = fe;
            Program.ExitFunc = (exitCode) => true;

            string[] args = new string[] { };
            Program._argsStr = Program.JoinArgs(args);

            // Act
            await Program.MainAsync();

            // Assert
            Assert.Equal(1, fp.RunCount);
            Assert.Equal(1, fp.RunCount);
        }

        [Fact]
        public void TestJoinArgs1()
        {
            // Arrange
            string[] args = new string[] { };
            
            // Act
            string str = Program.JoinArgs(args);

            // Assert
            Assert.Equal("", str);
        }

        [Fact]
        public void TestJoinArgs2()
        {
            // Arrange
            string[] args = new string[] { "KeyName", "mode=login", "test=value" };

            // Act
            string str = Program.JoinArgs(args);

            // Assert
            Assert.Equal("mode=login test=value", str);
        }
    }
}