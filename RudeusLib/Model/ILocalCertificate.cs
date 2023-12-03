using System.Security.Cryptography.X509Certificates;

namespace Rudeus.Model
{
    /// <summary>
    /// ローカルマシンの証明書を操作するモデルのインターフェース
    /// </summary>
    public interface ILocalCertificate
    {
        abstract static ILocalCertificate GetInstance();
        X509Certificate2? GetCertificate(string issuer);
        void InstallCertificateIntoRoot(string path);
        void InstallPkcs12IntoMy(string path, string password);
        void UninstallCertificateFromString(string certificate);
    }
}