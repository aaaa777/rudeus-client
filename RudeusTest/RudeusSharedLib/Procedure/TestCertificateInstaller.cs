using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;
using RudeusSharedLibTest.RudeusSharedLib.Model;

namespace RudeusTest.RudeusSharedLib.Procedure
{
    public class TestCertificateInstaller
    {
        [Fact]
        public void TestInstallDefaults()
        {
            CertificateInstaller.lc = FakeLocalCertificate.Create();

            CertificateInstaller.Run();
        }

        [Fact]
        public void TestRegistryInitializer()
        {
            RegistryInitializer.ConfSettings = FakeSettings.Create();
            RegistryInitializer.InnoSettings = FakeSettings.Create();
            RegistryInitializer.BgSettings = FakeSettings.Create();
            RegistryInitializer.BfSettings = FakeSettings.Create();

            RegistryInitializer.Run();
        }
    }
}
