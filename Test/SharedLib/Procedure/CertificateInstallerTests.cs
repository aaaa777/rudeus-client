using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;

namespace Rudeus.Procedure.Test
{
    public class CertificateInstallerTests
    {
        [Fact]
        public async void TestRun()
        {
            // Arrange
            var lc = FakeLocalCertificate.Create();
            CertificateInstaller ci = new CertificateInstaller(
                lc: lc
            );

            // Act
            await ci.Run();

            // Assert
            //Assert.True(lc.InstallCertificateIntoRootCalled);
        }
    }
}
