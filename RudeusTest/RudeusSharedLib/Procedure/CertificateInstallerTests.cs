using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;
using RudeusSharedLibTest.RudeusSharedLib.Model;

namespace RudeusSharedLibTest.RudeusSharedLib.Procedure
{
    public class CertificateInstallerTests
    {
        [Fact]
        public void TestRun()
        {
            var lc = FakeLocalCertificate.Create();
            CertificateInstaller ci = new CertificateInstaller(
                lc: lc
            );

            ci.Run();

            //Assert.True(lc.InstallCertificateIntoRootCalled);
        }
    }
}
