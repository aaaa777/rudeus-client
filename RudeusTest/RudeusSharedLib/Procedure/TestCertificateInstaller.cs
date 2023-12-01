using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;

namespace RudeusTest.RudeusSharedLib.Procedure
{
    public class TestCertificateInstaller
    {
        [Fact]
        public void TestInstallDefaults()
        {
            CertificateInstaller.lc = FakeLocalCertificate.GetInstance();

            CertificateInstaller.Run();
        }

        [Fact]
        public void TestRegistryInitializer()
        {
            RegistryInitializer.Settings = FakeSettings.GetInstance();

            RegistryInitializer.Run();
        }
    }
}
