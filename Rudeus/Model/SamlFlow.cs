using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using System.Net;

using System.Text.Json;
using Rudeus.Model.Response;
using Rudeus.Model.Request;
using System.Xml;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Runtime.ConstrainedExecution;

namespace Rudeus.Model
{
    internal class SamlFlow
    {
        private static readonly string _X509CertificateText = @"
MIIC4jCCAcoCCQC33wnybT5QZDANBgkqhkiG9w0BAQsFADAyMQswCQYDVQQGEwJV
SzEPMA0GA1UECgwGQm94eUhRMRIwEAYDVQQDDAlNb2NrIFNBTUwwIBcNMjIwMjI4
MjE0NjM4WhgPMzAyMTA3MDEyMTQ2MzhaMDIxCzAJBgNVBAYTAlVLMQ8wDQYDVQQK
DAZCb3h5SFExEjAQBgNVBAMMCU1vY2sgU0FNTDCCASIwDQYJKoZIhvcNAQEBBQAD
ggEPADCCAQoCggEBALGfYettMsct1T6tVUwTudNJH5Pnb9GGnkXi9Zw/e6x45DD0
RuRONbFlJ2T4RjAE/uG+AjXxXQ8o2SZfb9+GgmCHuTJFNgHoZ1nFVXCmb/Hg8Hpd
4vOAGXndixaReOiq3EH5XvpMjMkJ3+8+9VYMzMZOjkgQtAqO36eAFFfNKX7dTj3V
pwLkvz6/KFCq8OAwY+AUi4eZm5J57D31GzjHwfjH9WTeX0MyndmnNB1qV75qQR3b
2/W5sGHRv+9AarggJkF+ptUkXoLtVA51wcfYm6hILptpde5FQC8RWY1YrswBWAEZ
NfyrR4JeSweElNHg4NVOs4TwGjOPwWGqzTfgTlECAwEAATANBgkqhkiG9w0BAQsF
AAOCAQEAAYRlYflSXAWoZpFfwNiCQVE5d9zZ0DPzNdWhAybXcTyMf0z5mDf6FWBW
5Gyoi9u3EMEDnzLcJNkwJAAc39Apa4I2/tml+Jy29dk8bTyX6m93ngmCgdLh5Za4
khuU3AM3L63g7VexCuO7kwkjh/+LqdcIXsVGO6XDfu2QOs1Xpe9zIzLpwm/RNYeX
UjbSj5ce/jekpAw7qyVVL4xOyh8AtUW1ek3wIw1MJvEgEPt0d16oshWJpoS1OT8L
r/22SvYEo3EmSGdTVGgk3x3s+A0qWAqTcyjr7Q4s/GKYRFfomGwz0TZ4Iw1ZN99M
m0eo2USlSRTVl7QHRTuiuSThHpLKQQ==";

        /// <summary>
        /// URLをブラウザで開く
        /// </summary>
        /// 
        private static void OpenBrowser(string url)
        {
            ProcessStartInfo pi = new()
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(pi);
        }

        /// <summary>
        /// ブラウザでSAML認証を行い、ユーザー名を取得する
        /// </summary>
        /// <returns></returns>
        public static async Task<string> SAMLLoginAsync()
        {
            // SAML認証を行う

            string issuer = "https://httpbin.org/post";
            string displayName = "ipentec";
            // string issueXML = $"<samlp:AuthnRequest xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\"\n  xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"{displayName}\" Version=\"2.0\"\n  IssueInstant=\"2004-12-05T09:21:59Z\" AssertionConsumerServiceIndex=\"1\">\n\n  <saml:Issuer>\n{issuer}\n  </saml:Issuer>\n\n  <samlp:NameIDPolicy AllowCreate=\"true\"\n    Format=\"urn:oasis:names:tc:SAML:2.0:nameid-format:transient\"/>\n</samlp:AuthnRequest>";
            // string issueXML = $"<md:EntityDescriptor xmlns:md=\"urn:oasis:names:tc:SAML:2.0:metadata\" entityID=\"https://saml.example.com/entityid\" validUntil=\"2033-08-05T07:34:41.322Z\">\n<md:IDPSSODescriptor WantAuthnRequestsSigned=\"true\" protocolSupportEnumeration=\"urn:oasis:names:tc:SAML:2.0:protocol\">\n<md:KeyDescriptor use=\"signing\">\n<ds:KeyInfo xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\">\n<ds:X509Data>\n<ds:X509Certificate>MIIC4jCCAcoCCQC33wnybT5QZDANBgkqhkiG9w0BAQsFADAyMQswCQYDVQQGEwJV SzEPMA0GA1UECgwGQm94eUhRMRIwEAYDVQQDDAlNb2NrIFNBTUwwIBcNMjIwMjI4 MjE0NjM4WhgPMzAyMTA3MDEyMTQ2MzhaMDIxCzAJBgNVBAYTAlVLMQ8wDQYDVQQK DAZCb3h5SFExEjAQBgNVBAMMCU1vY2sgU0FNTDCCASIwDQYJKoZIhvcNAQEBBQAD ggEPADCCAQoCggEBALGfYettMsct1T6tVUwTudNJH5Pnb9GGnkXi9Zw/e6x45DD0 RuRONbFlJ2T4RjAE/uG+AjXxXQ8o2SZfb9+GgmCHuTJFNgHoZ1nFVXCmb/Hg8Hpd 4vOAGXndixaReOiq3EH5XvpMjMkJ3+8+9VYMzMZOjkgQtAqO36eAFFfNKX7dTj3V pwLkvz6/KFCq8OAwY+AUi4eZm5J57D31GzjHwfjH9WTeX0MyndmnNB1qV75qQR3b 2/W5sGHRv+9AarggJkF+ptUkXoLtVA51wcfYm6hILptpde5FQC8RWY1YrswBWAEZ NfyrR4JeSweElNHg4NVOs4TwGjOPwWGqzTfgTlECAwEAATANBgkqhkiG9w0BAQsF AAOCAQEAAYRlYflSXAWoZpFfwNiCQVE5d9zZ0DPzNdWhAybXcTyMf0z5mDf6FWBW 5Gyoi9u3EMEDnzLcJNkwJAAc39Apa4I2/tml+Jy29dk8bTyX6m93ngmCgdLh5Za4 khuU3AM3L63g7VexCuO7kwkjh/+LqdcIXsVGO6XDfu2QOs1Xpe9zIzLpwm/RNYeX UjbSj5ce/jekpAw7qyVVL4xOyh8AtUW1ek3wIw1MJvEgEPt0d16oshWJpoS1OT8L r/22SvYEo3EmSGdTVGgk3x3s+A0qWAqTcyjr7Q4s/GKYRFfomGwz0TZ4Iw1ZN99M m0eo2USlSRTVl7QHRTuiuSThHpLKQQ== </ds:X509Certificate>\n</ds:X509Data>\n</ds:KeyInfo>\n</md:KeyDescriptor>\n<md:NameIDFormat>urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress</md:NameIDFormat>\n<md:SingleSignOnService Binding=\"urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect\" Location=\"https://mocksaml.com/api/saml/sso\"/>\n<md:SingleSignOnService Binding=\"urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST\" Location=\"https://mocksaml.com/api/saml/sso\"/>\n</md:IDPSSODescriptor>\n</md:EntityDescriptor>";
            string issueXML = $"<samlp:AuthnRequest xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\" xmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"identifier_1\" Version=\"2.0\" IssueInstant=\"2004-12-05T09:21:59Z\" AssertionConsumerServiceIndex=\"1\">\r\n　　<saml:Issuer>https://sp.example.com/SAML2</saml:Issuer>\r\n　　<samlp:NameIDPolicy AllowCreate=\"true\" Format=\"urn:oasis:names:tc:SAML:2.0:nameid-format:transient\"/>\r\n</samlp:AuthnRequest>";

            string spEntryUrl = $"http://localhost:11177/saml/";
            string spListenerUrl = $"http://localhost:11178/";
            string idpUrl = $"https://mocksaml.com/api/saml/sso?redirect={spListenerUrl}";


            string encodedXML = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(issueXML));
            //string spRedirectUrl = $"{idpUrl}?SAMLRequest={encodedXML}&RelayState={System.Web.HttpUtility.UrlEncode(spListenerUrl)}";
            string spRedirectUrl = $"https://mocksaml.com/saml/login";

            // XMLをX509で署名する
            // https://docs.microsoft.com/ja-jp/dotnet/api/system.security.cryptography.xml.signedxml?view=net-5.0
            // https://docs.microsoft.com/ja-jp/dotnet/api/system.security.cryptography.xml.reference?view=net-5.0

            // https://stackoverflow.com/questions/39954624/c-sharp-x509-certificate-decoder
            byte[] bytes = Convert.FromBase64String(_X509CertificateText);
            X509Certificate2 cert = new(bytes);

            // https://learn.microsoft.com/ja-jp/dotnet/api/system.security.cryptography.xml.x509issuerserial?view=windowsdesktop-7.0
            //string formattedXML = SignXmlString(issueXML, cert);

            //string issueHTML = $"<html><body><form method=\"post\" action=\"{idpUrl}\"><input type=\"hidden\" name=\"SAMLRequest\" value=\"{System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(formattedXML))}\" /><input type=\"hidden\" name=\"RelayState\" value=\"{System.Web.HttpUtility.UrlEncode(spListenerUrl)}\" /><input type=\"submit\" value=\"Submit\" /></form></body></html>";
            string issueHTML = "";


            // spEntryUrl -> spRedirectUrl(IdP Login) -> spListenerUrl

            // ToDo: rudeus-client://rudeus/redirect?SAMLRequest=xxxx&RelayState=xxxxのようなURIスキームを使ったリダイレクトの実装
            // POST-Binding IdP redirect用のローカルサーバーを起動
            HttpListener listener = new();
            listener.Prefixes.Add(spEntryUrl);
            listener.Start();
            Task<HttpListenerContext> redirectResponseTask = listener.GetContextAsync();

            // リダイレクト先のローカルサーバーを起動
            HttpListener listener2 = new();
            listener2.Prefixes.Add(spListenerUrl);
            listener2.Start();
            Task<HttpListenerContext> idpResponseTask = listener2.GetContextAsync();

            // ブラウザで開く
            OpenBrowser(spEntryUrl);

            // IdPからのリダイレクトを待つ
            HttpListenerContext res = redirectResponseTask.Result;

            if (1 == 1)
            {
                res.Response.Redirect(spRedirectUrl);
            }
            else
            {
                res.Response.StatusCode = 200;
                res.Response.ContentType = "text/html";
                res.Response.ContentEncoding = System.Text.Encoding.UTF8;
                res.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(issueHTML));
                res.Response.OutputStream.Close();
            }
            res.Response.Close();
            listener.Stop();


            // idpからのレスポンスを待つ
            HttpListenerContext idpResponse = await idpResponseTask;
            string idpResponseText = new System.IO.StreamReader(idpResponse.Request.InputStream, System.Text.Encoding.UTF8).ReadToEnd();

            idpResponse.Response.Close();
            listener2.Stop();

            // idpからのレスポンスをパース
            string SAMLResponse = System.Web.HttpUtility.ParseQueryString(idpResponseText).Get("SAMLResponse");
            byte[] base64EncodedXML = System.Convert.FromBase64String(SAMLResponse);
            string decodedXML = System.Text.Encoding.UTF8.GetString(base64EncodedXML);
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(decodedXML);

            NameTable nt = new();
            XmlNamespaceManager nsmgr = new(nt);
            nsmgr.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");

            XmlNodeList subjectNodes = xmlDocument.SelectNodes("//saml:Subject", nsmgr);
            string nameId = "";
            foreach (XmlNode subjectNode in subjectNodes)
            {
                XmlNode nameIdNode = subjectNode.SelectSingleNode("saml:NameID", nsmgr);
                nameId = nameIdNode.InnerText;
                Console.WriteLine(nameId);
            }

            idpResponse.Response.Close();
            listener2.Stop();

            return nameId;
        }

        private static string SignXmlString(string xml, X509Certificate2 cert)
        {
            XmlDocument doc = new();
            doc.LoadXml(xml);
            SignedXml signedXml = new();
            signedXml.SigningKey = cert.GetRSAPublicKey();

            Reference reference = new();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Create a new KeyInfo object.
            KeyInfo keyInfo = new KeyInfo();

            // Load the certificate into a KeyInfoX509Data object
            // and add it to the KeyInfo object.
            // Create an X509IssuerSerial object and add it to the
            // KeyInfoX509Data object.

            KeyInfoX509Data kdata = new KeyInfoX509Data(cert);
            X509IssuerSerial xserial;

            xserial.IssuerName = cert.IssuerName.ToString();
            xserial.SerialNumber = cert.SerialNumber;

            kdata.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);

            keyInfo.AddClause(kdata);

            // Add the KeyInfo object to the SignedXml object.
            signedXml.KeyInfo = keyInfo;

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            MemoryStream ms = new();
            XmlTextWriter xtw = new(ms, System.Text.Encoding.UTF8);
            doc.WriteContentTo(xtw);
            xtw.Flush();
            ms.Flush();
            ms.Position = 0;

            string formattedXML = new System.IO.StreamReader(ms, System.Text.Encoding.UTF8).ReadToEnd();
            return formattedXML;
        }
    }
}
