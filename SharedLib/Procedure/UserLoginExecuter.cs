using Rudeus.API.Response;
using Rudeus.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SharedLib.Model.Settings;

namespace Rudeus.Procedure
{
    /// <summary>
    /// ユーザーのログインを行う手続き
    /// </summary>
    public class UserLoginExecuter : IProcedure
    {
        // DI for testing
        public static Func<string, bool> OpenBrowser { get; set; } = OpenWebPage;

        /// <inheritdoc/>
        public async Task Run()
        {
            string userIdOld = AppSettings.Username;
            Console.WriteLine("old user id:" + userIdOld);
            if (userIdOld == "")
            {
                await StartLoginFlow();
            }
        }

        private async Task StartLoginFlow()
        {
            try
            {
                // 学生IDを取得するためlocalhostでコールバックを待機
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ログイン画面を開く
                OpenBrowser(RemoteAPI.SamlLoginUrl);
                string userId = await userIdTask;

                // 管理サーバに送信
                LoginResponse res = RemoteAPI.LoginDevice(AppSettings.AccessToken, userId);
                AppSettings.Username = userId;
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
            return true;
        }
    }
}
