using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;

namespace RudeusBgForm
{
    partial class TaskTrayForm

    {
        //private System.Windows.Forms.ContextMenuStrip menu = new();
        //private System.Windows.Forms.NotifyIcon notifyIcon = new();

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskTrayForm));
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            testMessageToolStripMenuItem = new ToolStripMenuItem();
            testMessageToolStripMenuItem1 = new ToolStripMenuItem();
            testMassageToolStripMenuItem = new ToolStripMenuItem();
            tastSausageToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "クリック: Webポータルを開きます\r\n右クリック: メニュ―を開きます";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { testMessageToolStripMenuItem, testMassageToolStripMenuItem, tastSausageToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 92);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // testMessageToolStripMenuItem
            // 
            testMessageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testMessageToolStripMenuItem1 });
            testMessageToolStripMenuItem.Name = "testMessageToolStripMenuItem";
            testMessageToolStripMenuItem.Size = new Size(180, 22);
            testMessageToolStripMenuItem.Text = "操作";
            testMessageToolStripMenuItem.Click += testMessageToolStripMenuItem_Click;
            // 
            // testMessageToolStripMenuItem1
            // 
            testMessageToolStripMenuItem1.Name = "testMessageToolStripMenuItem1";
            testMessageToolStripMenuItem1.Size = new Size(188, 22);
            testMessageToolStripMenuItem1.Text = "学内アカウントでログイン";
            testMessageToolStripMenuItem1.Click += testMessageToolStripMenuItem1_Click;
            // 
            // testMassageToolStripMenuItem
            // 
            testMassageToolStripMenuItem.Name = "testMassageToolStripMenuItem";
            testMassageToolStripMenuItem.Size = new Size(180, 22);
            testMassageToolStripMenuItem.Text = "更新を確認";
            testMassageToolStripMenuItem.Click += testMassageToolStripMenuItem_Click;
            // 
            // tastSausageToolStripMenuItem
            // 
            tastSausageToolStripMenuItem.Name = "tastSausageToolStripMenuItem";
            tastSausageToolStripMenuItem.Size = new Size(180, 22);
            tastSausageToolStripMenuItem.Text = "終了";
            // 
            // TaskTrayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TaskTrayForm";
            Text = "TaskTrayForm";
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem testMessageToolStripMenuItem;
        private ToolStripMenuItem testMessageToolStripMenuItem1;
        private ToolStripMenuItem testMassageToolStripMenuItem;
        private ToolStripMenuItem tastSausageToolStripMenuItem;
    }
}