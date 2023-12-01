﻿using Rudeus.API;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{

    public class CertificateInstaller : IProcedure
    {
        public static ILocalCertificate lc { get; set; } = LocalCertificate.GetInstance();
        public void Run()
        {
#if (DEVELOPMENT)
            Console.WriteLine("[Installer] Cert: Certificate installation is skipped because this is Debug build.");
            Console.WriteLine("[Installer] Cert: There is 2ways to install Certificate, execute Release build or install manually.");
#else
            string capath = "ca.crt";
            string p12path = "stu2.p12";
        
            lc.InstallCertificateIntoRoot(capath);
            lc.InstallPkcs12IntoMy(p12path, "exampleexampleexample");

            Console.WriteLine("[Installer] Cert: Certificate is installed successfully");
#endif
        }
    }

}

