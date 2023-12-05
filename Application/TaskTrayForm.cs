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
                ToolStripMenuItemLoginStatus.Text = $"s{AppSettings.Username}�Ƃ��ă��O�C����";
            }
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
            string userIdOld = AppSettings.Username;
            try
            {
                // �w��ID���擾���邽��localhost�ŃR�[���o�b�N��ҋ@
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ���O�C����ʂ��J��
                OpenWebPage(RemoteAPI.SamlLoginUrl);
                string userId = await userIdTask;

                // �Ǘ��T�[�o�ɑ��M
                LoginResponse res = RemoteAPI.LoginDevice(AppSettings.AccessToken, userId);
                AppSettings.Username = userId;

                ToolStripMenuItemLoginStatus.Text = $"s{userId}�Ƃ��ă��O�C����";
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

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            // �I��
            System.Windows.Forms.Application.Exit();
        }

        private void ToolStripMenuItemLinkPortal_Click(object sender, EventArgs e)
        {
            // �|�[�^�����J��
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
            // �������ς�
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ���O�A�E�g����
            AppSettings.Username = string.Empty;
            ToolStripMenuItemLoginStatus.Text = "���O�C�����Ă��܂���";
        }
    }
}