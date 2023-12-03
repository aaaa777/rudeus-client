using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;

namespace RudeusSharedLibTest.Procedure
{
    public class CertificateInstallerTests
    {
        [Fact]
        public async void TestRun()
        {
            var lc = FakeLocalCertificate.Create();
            CertificateInstaller ci = new CertificateInstaller(
                lc: lc
            );

            await ci.Run();

            //Assert.True(lc.InstallCertificateIntoRootCalled);
        }
    }
}
