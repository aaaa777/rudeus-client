using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Model;
using Rudeus.Procedure;
using SharedLib.Model.Settings;
using Test.SharedLib.Model;
using Test.SharedLib.Model.Settings;

namespace Test.SharedLib.Procedure
{
    public class LocalMachineInfoUpdaterTests
    {
        [Fact]
        public async Task TestRun()
        {
            // Arrange
            IRootSettings frs = new FakeSettings();
            ILocalMachine flm = new FakeLocalMachine();
            var lmiu = new LocalMachineInfoUpdater(settings: frs, localMachine: flm);
            
            // Act
            await lmiu.Run();

            // Assert
            Assert.Equal(frs.HostnameP, flm.GetHostname());
            Assert.Equal(frs.LabelIdP, flm.GetLabelId());
            Assert.Equal(frs.SpecP, flm.GetSpec());
            Assert.Equal(frs.CpuNameP, flm.GetCpuName());
            Assert.Equal(frs.MemoryP, flm.GetMemory());
            Assert.Equal(frs.CDriveP, flm.GetCDrive());
            Assert.Equal(frs.OSP, flm.GetOS());
            Assert.Equal(frs.OSVersionP, flm.GetOSVersion());
            Assert.Equal(frs.WithSecureP, flm.GetWithSecure());
        }
    }
}
