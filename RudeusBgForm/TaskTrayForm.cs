using System.Reflection;
using System.Windows.Forms;
using Rudeus.Model;
using Rudeus.Model.Response;

namespace RudeusBgForm
{
    public partial class TaskTrayForm : Form
    {
        public TaskTrayForm()
        {
            InitializeComponent();
            taskTrayIcon.MouseClick += notifyIcon1_Click;
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void testMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            string userIdOld = Settings.Username;
            try
            {
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ログイン画面を開く
                OpenWebPage(RemoteAPI.SamlLoginUrl);
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

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 右クリックメニューを開いたとき
        }

        private void tastSausageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 終了
            Application.Exit();
        }

        private void ポータルToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ポータルを開く
            OpenWebPage(Utils.WebPortalUrl);
        }

        private void pOLITE3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWebPage(Utils.Polite3Url);
        }

        private void 教務情報WebシステムToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWebPage(Utils.KyoumuUrl);
        }

        private void s9999999としてログイン中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // データバインドする
        }
    }
}