using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class RudeusCert
{
    public static void InstallCertificate()
    {
        string capath = "ca.crt";
        string p12path = "stu2.p12";
#if (DEBUG)
        capath = @"C:\Users\a774n\source\repos\Rudeus\ca.crt";
        p12path = @"C:\Users\a774n\source\repos\Rudeus\stu2.p12";
#endif
        CertificateAPI.InstallCertificateIntoRoot(capath);
        CertificateAPI.InstallPkcs12IntoMy(p12path, "exampleexampleexample");

        Console.WriteLine("Certificate is installed successfully");
    }

}

