using System.Windows.Forms;
using Rudeus.Model;

namespace RudeusBgForm
{
    public partial class TaskTrayForm : Form
    {
        public TaskTrayForm()
        {
            InitializeComponent();
            notifyIcon1.MouseClick += notifyIcon1_Click;
        }

        private void notifyIcon1_Click(object sender, MouseEventArgs e)
        {
            // �|�[�^�����J��
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void testMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testMassageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �w���A�J�E���g�Ń��O�C���̃{�^��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testMessageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // �E�N���b�N���j���[���J�����Ƃ�
        }
    }
}