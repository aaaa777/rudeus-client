using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Rudeus.Model
{
    internal class CertificateAPI
    {
        public static void InstallPkcs12IntoMy(string path, string password)
        {
            X509Certificate2 cert = new(path, password);
            X509Store store = new(StoreName.My);
            store.Open(OpenFlags.ReadWrite);
            store.Add(cert);
        }

        // popup warning
        public static void InstallCertificateIntoRoot(string path)
        {
            X509Certificate2 cert = new(path);
            X509Store store = new(StoreName.Root);
            store.Open(OpenFlags.ReadWrite);
            store.Add(cert);
        }

        public static void UninstallCertificateFromString(string certificate)
        {

        }


        public static X509Certificate2? GetCertificate(string issuer) 
        {
            // https://qiita.com/jkomatsu/items/27e2527ac3f176854d6e
            X509Store store = new(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            
            // 証明書を選ぶ（絞り込む）、まずは有効期間内のもの
            // 最後のtrueは証明のパスが正式なもののみ取り出す指定なので、オレオレの場合はfalseにする
            X509Certificate2Collection col_all = store.Certificates;
            X509Certificate2Collection col_date = col_all.Find(X509FindType.FindByTimeValid, DateTime.Now, true);

            // 次に署名者の名前（CA証明書のCNなど）で絞り込む
            X509Certificate2Collection col_issuer = col_date.Find(X509FindType.FindByIssuerName, issuer, true);

            // 見つかった証明書を出力
            // X509Certificate2のオブジェクトなので、HttpClientとかでクライアント証明書として使える
            foreach (X509Certificate2 cert in col_issuer)
            {
                Console.WriteLine(cert.ToString());
                return cert;
            }
            return null;
        }
    }
}
