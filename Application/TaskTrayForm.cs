using System.Reflection;
using System.Windows.Forms;
using Rudeus.API;
using Rudeus.API.Response;
using Rudeus;
using SharedLib.Model.Settings;

namespace Rudeus.Application
{
    public partial class TaskTrayForm : Form
    {
        public TaskTrayForm()
        {
            InitializeComponent();
            taskTrayIcon.MouseClick += notifyIcon1_Click;
            if (AppSettings.Username != "")
            {
                ToolStripMenuItemLoginStatus.Text = $"s{AppSettings.Username}としてログイン中";
            }
        }

        private void notifyIcon1_Click(object? sender, MouseEventArgs e)
        {
            // アイコンをクリックしたときにも右クリックメニューを表示する
            // https://akamist.com/blog/archives/1243
            if (e.Button == MouseButtons.Left)
            {
                var method = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                {
                    method.Invoke(taskTrayIcon, null);
                }
            }
        }

        private void testMassageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 更新を確認
                Notificate();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 学内アカウントでログインのボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void testMessageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string userIdOld = AppSettings.Username;
            try
            {
                // 学生IDを取得するためlocalhostでコールバックを待機
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ログイン画面を開く
                OpenWebPage(RemoteAPI.SamlLoginUrl);
                string userId = await userIdTask;

                // 管理サーバに送信
                LoginResponse res = RemoteAPI.LoginDevice(AppSettings.AccessToken, userId);
                AppSettings.Username = userId;

                ToolStripMenuItemLoginStatus.Text = $"s{userId}としてログイン中";
            }
            catch
            {
                // ログインして送信に失敗
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 右クリックメニューを開いたとき
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            // 終了
            System.Windows.Forms.Application.Exit();
        }

        private void ToolStripMenuItemLinkPortal_Click(object sender, EventArgs e)
        {
            // ポータルを開く
            OpenWebPage(Utils.WebPortalUrl);
        }

        private void ToolStripMenuItemLinkPolite3_Click(object sender, EventArgs e)
        {
            OpenWebPage(Utils.Polite3Url);
        }

        private void ToolStripMenuItemLinkKyomu_Click(object sender, EventArgs e)
        {
            OpenWebPage(Utils.KyoumuUrl);
        }

        private void ToolStripMenuItemLoginStatus_Click(object sender, EventArgs e)
        {
            // 無効化済み
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ログアウト処理
            AppSettings.Username = string.Empty;
            ToolStripMenuItemLoginStatus.Text = "ログインしていません";
        }
    }
}