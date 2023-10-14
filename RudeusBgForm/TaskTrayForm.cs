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
            // �A�C�R�����N���b�N�����Ƃ��ɂ��E�N���b�N���j���[��\������
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
                // �X�V���m�F
                Notificate();
            }
            catch
            {

            }
        }

        /// <summary>
        /// �w���A�J�E���g�Ń��O�C���̃{�^��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void testMessageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string userIdOld = Settings.Username;
            try
            {
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ���O�C����ʂ��J��
                OpenWebPage(RemoteAPI.SamlLoginUrl);
                string userId = await userIdTask;

                // �Ǘ��T�[�o�ɑ��M
                LoginResponse res = RemoteAPI.LoginDevice(Settings.AccessToken, userId);
                Settings.Username = userId;
            }
            catch
            {
                // ���O�C�����đ��M�Ɏ��s
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �E�N���b�N���j���[���J�����Ƃ�
        }

        private void tastSausageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // �I��
            Application.Exit();
        }

        private void �|�[�^��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // �|�[�^�����J��
            OpenWebPage(Utils.WebPortalUrl);
        }

        private void pOLITE3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWebPage(Utils.Polite3Url);
        }

        private void �������Web�V�X�e��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenWebPage(Utils.KyoumuUrl);
        }

        private void s9999999�Ƃ��ă��O�C����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // �f�[�^�o�C���h����
        }
    }
}