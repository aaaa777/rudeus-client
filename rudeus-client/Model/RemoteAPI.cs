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
using rudeus_client.Model.Response;
using rudeus_client.Model.Request;
using System.Xml;

namespace rudeus_client.Model
{
 
    /// <summary>
    /// 管理サーバとREST APTで通信する、モデルのプロパティを読み取るが書き込みはしない
    /// </summary>
    internal class RemoteAPI
    {
        /// <summary>
        /// Todo: クライアント証明書を追加する
        /// https://stackoverflow.com/questions/40014047/add-client-certificate-to-net-core-httpclient
        /// </summary>
        private static readonly HttpClientHandler handler = new()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12,
        //    ClientCertificates = new X509CertificateCollection(),
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        private static readonly HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };

        private static string Request(string accessToken, string requestPath, BaseRequest requestStruct)
        {
            var payload = JsonSerializer.Serialize(requestStruct);
            Console.WriteLine($"Request: {payload}");

            var request = new HttpRequestMessage(HttpMethod.Post, requestPath);
            // using HttpResponseMessage response = sharedClient.GetAsync($"todos/1").Result;

            var dummyResponse = $"{{\"Status\":\"ok\",\"ResponseData\": {{\"AccessToken\": \"abcvgjsdfgh\"}}}}";
            return dummyResponse;
            // return response.Content.ReadAsStringAsync().Result;
        }

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
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(Device device)
        {
            RegisterRequest req = new(device.DeviceId, device.DeviceName);
            var response = Request("", "/api/register", req);
            RegisterResponse res = JsonSerializer.Deserialize<RegisterResponse>(response);
            return res;
        }

        /// <summary>
        /// デバイスIDとアクセストークンを利用してデバイス情報を更新する
        /// </summary>
        /// 
        public static UpdateResponse UpdateDevice(Device device)
        {
            UpdateRequest req = new(device.AccessToken, device.Username);
            var response = Request(device.AccessToken, "/api/update", req);
            return JsonSerializer.Deserialize<UpdateResponse>(response);
        }

        /// <summary>
        /// ログインして紐づけを行う
        /// </summary>
        /// 
        public static async Task<LoginResponse> LoginDevice(Device device)
        {
            // SAML認証を行う
            // Todo：SAMLで取れたユーザー名か管理サーバで取れたユーザー名のどちらを利用する？
            // OpenBrowser("http://www.ipentec.com");
            // Request(device.AccessToken, "/api/saml_listener", @"{""type"": ""アサーションの送信""}");
            string userId = await SAMLLoginAsync();

            // 取得したユーザー名を送信する
            LoginRequest req = new(device.AccessToken, userId);
            //var response = Request(device.AccessToken, "/api/update", $"{{\"type\": \"update\", \"request_data\": {{ \"username\": \"{device.Username}\"}} }}");
            var response = Request(device.AccessToken, "/api/login", req);

            LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(response);
            loginResponse.ResponseData.Username = userId;
            return loginResponse;
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
            string idpUrl = "https://mocksaml.com/api/saml/sso";
            
            string issueHTML = $"<html><body><form method=\"post\" action=\"{idpUrl}\"><input type=\"hidden\" name=\"SAMLRequest\" value=\"{System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(issueXML))}\" /><input type=\"hidden\" name=\"RelayState\" value=\"{System.Web.HttpUtility.UrlEncode(spListenerUrl)}\" /><input type=\"submit\" value=\"Submit\" /></form></body></html>";

            string encodedXML = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(issueXML));
            //string spRedirectUrl = $"{idpUrl}?SAMLRequest={encodedXML}&RelayState={System.Web.HttpUtility.UrlEncode(spListenerUrl)}";
            string spRedirectUrl = $"https://mocksaml.com/saml/login";

            // spEntryUrl -> spRedirectUrl(IdP Login) -> spListenerUrl

            // POST-Binding IdP redirect用のローカルサーバーを起動
            HttpListener listener = new();
            listener.Prefixes.Add(spEntryUrl);
            listener.Start();
            Task<HttpListenerContext> redirectResponseTask = listener.GetContextAsync();



            // ブラウザで開く
            OpenBrowser(spEntryUrl);

            // IdPからのリダイレクトを待つ
            HttpListenerContext res = redirectResponseTask.Result;

            if(true)
            {
                res.Response.Redirect(spRedirectUrl);
            } else {
                res.Response.StatusCode = 200;
                res.Response.ContentType = "text/html";
                res.Response.ContentEncoding = System.Text.Encoding.UTF8;
                res.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(issueHTML));
                res.Response.OutputStream.Close();
            }
            res.Response.Close();
            listener.Stop();

            HttpListener listener2 = new();
            listener2.Prefixes.Add(spListenerUrl);
            listener2.Start();
            Task<HttpListenerContext> idpResponseTask = listener2.GetContextAsync();

            HttpListenerContext idpResponse = await idpResponseTask;
            string idpResponseText = new System.IO.StreamReader(idpResponse.Request.InputStream, System.Text.Encoding.UTF8).ReadToEnd();

            idpResponse.Response.Close();
            listener2.Stop();

            string SAMLResponse = System.Web.HttpUtility.ParseQueryString(idpResponseText).Get("SAMLResponse");
            byte[] base64EncodedXML = System.Convert.FromBase64String(SAMLResponse);
            string decodedXML = System.Text.Encoding.UTF8.GetString(base64EncodedXML);
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(decodedXML);

            NameTable nt = new NameTable();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
            nsmgr.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");

            XmlNodeList subjectNodes = xmlDocument.SelectNodes("//saml:Subject", nsmgr);
            string nameId = "";
            foreach(XmlNode subjectNode in subjectNodes)
            {
                XmlNode nameIdNode = subjectNode.SelectSingleNode("saml:NameID", nsmgr);
                nameId = nameIdNode.InnerText;
                Console.WriteLine(nameId);
            }

            idpResponse.Response.Close();
            listener2.Stop();

            return nameId;
        }
    }
}
