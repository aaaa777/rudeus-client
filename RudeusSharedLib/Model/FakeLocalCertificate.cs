using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model
{
    internal class FakeLocalCertificate : ILocalCertificate
    {
        public static ILocalCertificate _fakeLocalCertificate = new FakeLocalCertificate();
        public static ILocalCertificate GetInstance() { return _fakeLocalCertificate; }

        public static ILocalCertificate Create() { return new FakeLocalCertificate(); }

        private FakeLocalCertificate() { }

        public X509Certificate2 GetCertificate(string issuer)
        {
            throw new NotImplementedException();
        }

        public void InstallCertificateIntoRoot(string path) { }

        public void InstallPkcs12IntoMy(string path, string password) { }

        public void UninstallCertificateFromString(string certificate) { }
    }
}
