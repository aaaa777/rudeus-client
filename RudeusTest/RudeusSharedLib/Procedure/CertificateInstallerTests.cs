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
            CertificateInstaller ci = new CertificateInstaller();
            ci._localCertificate = FakeLocalCertificate.Create();

            ci.Run();
        }
    }
}
