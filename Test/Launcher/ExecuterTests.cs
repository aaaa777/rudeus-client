using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Launcher;
using Rudeus.Launcher.Procedure;
using Test.SharedLib.Model;
using SharedLib.Model.Settings;
using Test.SharedLib.Model.Settings;

namespace Test.Launcher
{
    public class ExecuterTests
    {
        // TODO: このヘルパーメソッド部分のテストが必要なら新規クラスを作成する
        private int StartProcessCount = 0;
        private int FakeStartProcessFailure(string exePath, string args)
        {
            StartProcessCount++;
            return -1;
        }
        private int FakeStartProcessSuccess(string exePath, string args)
        {
            StartProcessCount++;
            return 0;
        }

        [Fact]
        public async Task TestRun()
        {
            StartProcessCount = 0;
            IAppSettings appSetting = new FakeSettings();
            var launcher = new Executer(aps: appSetting, args: "", startProcess: FakeStartProcessSuccess);

            await launcher.RunExe();

            Assert.Equal(0, launcher.ExitCode);
            Assert.Equal(1, StartProcessCount);
        }

        [Fact]
        public async Task TestRun2()
        {
            StartProcessCount = 0;
            IAppSettings appSetting = new FakeSettings();
            appSetting.SetLatestVersionStatusUnlaunchableP();
            var launcher = new Executer(aps: appSetting, args: "", startProcess: FakeStartProcessSuccess);

            await launcher.RunExe();

            Assert.Equal(0, launcher.ExitCode);
            Assert.Equal(1, StartProcessCount);
        }

        [Fact]
        public async Task TestRun3()
        {
            StartProcessCount = 0;
            IAppSettings appSetting = new FakeSettings();
            var launcher = new Executer(aps: appSetting, args: "", startProcess: FakeStartProcessFailure);

            await launcher.RunExe();
            //var exception = await Record.ExceptionAsync(launcher.Run);

            Assert.Equal(-1, launcher.ExitCode);
            Assert.Equal(2, StartProcessCount);
        }
    }
}
