using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    /// <summary>
    /// ローカルマシンの証明書を操作するモデルのFake実装
    /// </summary>
    public class FakeLocalCertificate : ILocalCertificate
    {
        public static ILocalCertificate _fakeLocalCertificate = new FakeLocalCertificate();

        /// <summary>
        /// 単一のインスタンスを取得する
        /// </summary>
        /// <returns></returns>
        public static ILocalCertificate GetInstance() { return _fakeLocalCertificate; }

        /// <summary>
        /// 新しいインスタンスを生成する
        /// </summary>
        /// <returns></returns>
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
