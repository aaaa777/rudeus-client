using Rudeus.API.Response;
using Rudeus.API;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Rudeus.Procedure
{
    internal class StartUserLoginFlow : IProcedure
    {
        public static Func<string, bool> OpenBrowser { get; set; } = OpenWebPage;

        public static async void Run()
        {
            string userIdOld = Settings.Username;
            Console.WriteLine("old user id:" + userIdOld);
            if (userIdOld == "")
            {
                await StartLoginFlow();
            }
        }

        private static async Task StartLoginFlow()
        {
            try
            {
                // 学生IDを取得するためlocalhostでコールバックを待機
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ログイン画面を開く
                OpenBrowser(RemoteAPI.SamlLoginUrl);
                string userId = await userIdTask;

                // 管理サーバに送信
                LoginResponse res = RemoteAPI.LoginDevice(Settings.AccessToken, userId);
                Settings.Username = userId;
            }
            catch
            {
                // ログインして送信に失敗
            }
        }

        private static bool OpenWebPage(string url)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
