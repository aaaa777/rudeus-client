using Microsoft.VisualBasic.ApplicationServices;
using Rudeus.Model;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Toolkit.Uwp.Notifications;

namespace RudeusBgForm
{
    partial class TaskTrayForm

    {
        //private System.Windows.Forms.ContextMenuStrip menu = new();
        //private System.Windows.Forms.NotifyIcon notifyIcon = new();

        private void OpenWebPage(string url)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void Notificate()
        {
            // https://zenn.dev/ambleside138/articles/75e7f8fdcf4fdc
            new ToastContentBuilder()
                .AddText("HIU manager")
                .AddText("更新はありません")
                .Show();
        }

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
            taskTrayIcon = new NotifyIcon(components);
            iconMenu = new ContextMenuStrip(components);
            testMessageToolStripMenuItem = new ToolStripMenuItem();
            testMessageToolStripMenuItem1 = new ToolStripMenuItem();
            リンク集ToolStripMenuItem = new ToolStripMenuItem();
            ポータルToolStripMenuItem = new ToolStripMenuItem();
            pOLITE3ToolStripMenuItem = new ToolStripMenuItem();
            教務情報WebシステムToolStripMenuItem = new ToolStripMenuItem();
            testMassageToolStripMenuItem = new ToolStripMenuItem();
            tastSausageToolStripMenuItem = new ToolStripMenuItem();
            s9999999としてログイン中ToolStripMenuItem = new ToolStripMenuItem();
            iconMenu.SuspendLayout();
            SuspendLayout();
            // 
            // taskTrayIcon
            // 
            taskTrayIcon.ContextMenuStrip = iconMenu;
            taskTrayIcon.Icon = (Icon)resources.GetObject("taskTrayIcon.Icon");
            taskTrayIcon.Text = "クリック: Webポータルを開きます\r\n右クリック: メニュ―を開きます";
            taskTrayIcon.Visible = true;
            taskTrayIcon.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // iconMenu
            // 
            iconMenu.ImageScalingSize = new Size(20, 20);
            iconMenu.Items.AddRange(new ToolStripItem[] { testMessageToolStripMenuItem, リンク集ToolStripMenuItem, testMassageToolStripMenuItem, tastSausageToolStripMenuItem });
            iconMenu.Name = "contextMenuStrip1";
            iconMenu.Size = new Size(181, 114);
            iconMenu.Opening += contextMenuStrip1_Opening;
            // 
            // testMessageToolStripMenuItem
            // 
            testMessageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { s9999999としてログイン中ToolStripMenuItem, testMessageToolStripMenuItem1 });
            testMessageToolStripMenuItem.Name = "testMessageToolStripMenuItem";
            testMessageToolStripMenuItem.Size = new Size(180, 22);
            testMessageToolStripMenuItem.Text = "ログイン";
            testMessageToolStripMenuItem.Click += testMessageToolStripMenuItem_Click;
            // 
            // testMessageToolStripMenuItem1
            // 
            testMessageToolStripMenuItem1.Name = "testMessageToolStripMenuItem1";
            testMessageToolStripMenuItem1.Size = new Size(202, 22);
            testMessageToolStripMenuItem1.Text = "HIUPC Managerにログイン";
            testMessageToolStripMenuItem1.Click += testMessageToolStripMenuItem1_Click;
            // 
            // リンク集ToolStripMenuItem
            // 
            リンク集ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ポータルToolStripMenuItem, pOLITE3ToolStripMenuItem, 教務情報WebシステムToolStripMenuItem });
            リンク集ToolStripMenuItem.Name = "リンク集ToolStripMenuItem";
            リンク集ToolStripMenuItem.Size = new Size(180, 22);
            リンク集ToolStripMenuItem.Text = "リンク集";
            // 
            // ポータルToolStripMenuItem
            // 
            ポータルToolStripMenuItem.Name = "ポータルToolStripMenuItem";
            ポータルToolStripMenuItem.Size = new Size(183, 22);
            ポータルToolStripMenuItem.Text = "Webポータル";
            ポータルToolStripMenuItem.Click += ポータルToolStripMenuItem_Click;
            // 
            // pOLITE3ToolStripMenuItem
            // 
            pOLITE3ToolStripMenuItem.Name = "pOLITE3ToolStripMenuItem";
            pOLITE3ToolStripMenuItem.Size = new Size(183, 22);
            pOLITE3ToolStripMenuItem.Text = "POLITE3";
            pOLITE3ToolStripMenuItem.Click += pOLITE3ToolStripMenuItem_Click;
            // 
            // 教務情報WebシステムToolStripMenuItem
            // 
            教務情報WebシステムToolStripMenuItem.Name = "教務情報WebシステムToolStripMenuItem";
            教務情報WebシステムToolStripMenuItem.Size = new Size(183, 22);
            教務情報WebシステムToolStripMenuItem.Text = "教務情報Webシステム";
            教務情報WebシステムToolStripMenuItem.Click += 教務情報WebシステムToolStripMenuItem_Click;
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
            tastSausageToolStripMenuItem.Click += tastSausageToolStripMenuItem_Click;
            // 
            // s9999999としてログイン中ToolStripMenuItem
            // 
            s9999999としてログイン中ToolStripMenuItem.Enabled = false;
            s9999999としてログイン中ToolStripMenuItem.Name = "s9999999としてログイン中ToolStripMenuItem";
            s9999999としてログイン中ToolStripMenuItem.Size = new Size(202, 22);
            s9999999としてログイン中ToolStripMenuItem.Text = "s9999999としてログイン中";
            s9999999としてログイン中ToolStripMenuItem.Click += s9999999としてログイン中ToolStripMenuItem_Click;
            // 
            // TaskTrayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(210, 16);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TaskTrayForm";
            Text = "TaskTrayForm";
            iconMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon taskTrayIcon;
        private ContextMenuStrip iconMenu;
        private ToolStripMenuItem testMessageToolStripMenuItem;
        private ToolStripMenuItem testMessageToolStripMenuItem1;
        private ToolStripMenuItem testMassageToolStripMenuItem;
        private ToolStripMenuItem tastSausageToolStripMenuItem;
        private ToolStripMenuItem リンク集ToolStripMenuItem;
        private ToolStripMenuItem ポータルToolStripMenuItem;
        private ToolStripMenuItem pOLITE3ToolStripMenuItem;
        private ToolStripMenuItem 教務情報WebシステムToolStripMenuItem;
        private ToolStripMenuItem s9999999としてログイン中ToolStripMenuItem;
    }
}