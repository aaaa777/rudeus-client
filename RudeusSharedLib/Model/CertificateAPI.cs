using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal class CertificateAPI
    {
        static void InstallCertificateFromString(string certificate)
        {
            if (string.IsNullOrEmpty(certificate))
            {
                throw new ArgumentNullException();
            }
            return;
        }

        static void UninstallCertificateFromString(string certificate)
        {

        }

    }
}
