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
    /// ログイン(ユーザ紐付)とラベルID入力を促す
    /// </summary>
    public class UserLoginExecuter : IProcedure
    {
        // DI for testing
        // TODO: コンストラクタ
        public IRootSettings RootSettings { get; set; } = new RootSettings();
        public static Func<string, bool> OpenBrowser { get; set; } = OpenWebPage;

        /// <inheritdoc/>
        public async Task Run()
        {
            string userIdOld = RootSettings.UsernameP;
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
                // label id を取得
                var res = RemoteAPI.GetLabelId(RootSettings.AccessTokenP);
                var labelId = res.response_data.label_id;
                Console.WriteLine("label id: " + labelId);

                // label id がない場合はログイン画面を開く
                if(labelId == null || labelId == "")
                {
                    // ログイン画面を開く
                    OpenBrowser(Utils.buildLabelIdUpdateUrl(RootSettings.DeviceIdP));
                }
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
