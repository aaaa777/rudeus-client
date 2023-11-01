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
            ToolStripMenuItemLoginNav = new ToolStripMenuItem();
            ToolStripMenuItemLoginStatus = new ToolStripMenuItem();
            ToolStripMenuItemLoginButton = new ToolStripMenuItem();
            ログアウトToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItemLinkList = new ToolStripMenuItem();
            ToolStripMenuItemLinkPortal = new ToolStripMenuItem();
            ToolStripMenuItemLinkPolite3 = new ToolStripMenuItem();
            ToolStripMenuItemLinkKyomu = new ToolStripMenuItem();
            ToolStripMenuItemCheckUpdate = new ToolStripMenuItem();
            ToolStripMenuItemExit = new ToolStripMenuItem();
            iconMenu.SuspendLayout();
            SuspendLayout();
            // 
            // taskTrayIcon
            // 
            taskTrayIcon.ContextMenuStrip = iconMenu;
            taskTrayIcon.Icon = (Icon)resources.GetObject("taskTrayIcon.Icon");
            taskTrayIcon.Text = "クリックでメニュ―を開きます";
            taskTrayIcon.Visible = true;
            // 
            // iconMenu
            // 
            iconMenu.ImageScalingSize = new Size(20, 20);
            iconMenu.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemLoginNav, ToolStripMenuItemLinkList, ToolStripMenuItemCheckUpdate, ToolStripMenuItemExit });
            iconMenu.Name = "contextMenuStrip1";
            iconMenu.Size = new Size(181, 114);
            iconMenu.Opening += contextMenuStrip1_Opening;
            // 
            // ToolStripMenuItemLoginNav
            // 
            ToolStripMenuItemLoginNav.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemLoginStatus, ToolStripMenuItemLoginButton, ログアウトToolStripMenuItem });
            ToolStripMenuItemLoginNav.Name = "ToolStripMenuItemLoginNav";
            ToolStripMenuItemLoginNav.Size = new Size(180, 22);
            ToolStripMenuItemLoginNav.Text = "ログイン";
            // 
            // ToolStripMenuItemLoginStatus
            // 
            ToolStripMenuItemLoginStatus.Enabled = false;
            ToolStripMenuItemLoginStatus.Name = "ToolStripMenuItemLoginStatus";
            ToolStripMenuItemLoginStatus.Size = new Size(199, 22);
            ToolStripMenuItemLoginStatus.Text = "ログインしていません";
            ToolStripMenuItemLoginStatus.Click += ToolStripMenuItemLoginStatus_Click;
            // 
            // ToolStripMenuItemLoginButton
            // 
            ToolStripMenuItemLoginButton.Name = "ToolStripMenuItemLoginButton";
            ToolStripMenuItemLoginButton.Size = new Size(199, 22);
            ToolStripMenuItemLoginButton.Text = "情報大アカウントでログイン";
            ToolStripMenuItemLoginButton.Click += testMessageToolStripMenuItem1_Click;
            // 
            // ログアウトToolStripMenuItem
            // 
            ログアウトToolStripMenuItem.Name = "ログアウトToolStripMenuItem";
            ログアウトToolStripMenuItem.Size = new Size(199, 22);
            ログアウトToolStripMenuItem.Text = "ログアウト";
            ログアウトToolStripMenuItem.Click += ログアウトToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemLinkList
            // 
            ToolStripMenuItemLinkList.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemLinkPortal, ToolStripMenuItemLinkPolite3, ToolStripMenuItemLinkKyomu });
            ToolStripMenuItemLinkList.Name = "ToolStripMenuItemLinkList";
            ToolStripMenuItemLinkList.Size = new Size(180, 22);
            ToolStripMenuItemLinkList.Text = "リンク集";
            // 
            // ToolStripMenuItemLinkPortal
            // 
            ToolStripMenuItemLinkPortal.Name = "ToolStripMenuItemLinkPortal";
            ToolStripMenuItemLinkPortal.Size = new Size(183, 22);
            ToolStripMenuItemLinkPortal.Text = "Webポータル";
            ToolStripMenuItemLinkPortal.Click += ToolStripMenuItemLinkPortal_Click;
            // 
            // ToolStripMenuItemLinkPolite3
            // 
            ToolStripMenuItemLinkPolite3.Name = "ToolStripMenuItemLinkPolite3";
            ToolStripMenuItemLinkPolite3.Size = new Size(183, 22);
            ToolStripMenuItemLinkPolite3.Text = "POLITE3";
            ToolStripMenuItemLinkPolite3.Click += ToolStripMenuItemLinkPolite3_Click;
            // 
            // ToolStripMenuItemLinkKyomu
            // 
            ToolStripMenuItemLinkKyomu.Name = "ToolStripMenuItemLinkKyomu";
            ToolStripMenuItemLinkKyomu.Size = new Size(183, 22);
            ToolStripMenuItemLinkKyomu.Text = "教務情報Webシステム";
            ToolStripMenuItemLinkKyomu.Click += ToolStripMenuItemLinkKyomu_Click;
            // 
            // ToolStripMenuItemCheckUpdate
            // 
            ToolStripMenuItemCheckUpdate.Name = "ToolStripMenuItemCheckUpdate";
            ToolStripMenuItemCheckUpdate.Size = new Size(180, 22);
            ToolStripMenuItemCheckUpdate.Text = "更新を確認";
            ToolStripMenuItemCheckUpdate.Click += testMassageToolStripMenuItem_Click;
            // 
            // ToolStripMenuItemExit
            // 
            ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            ToolStripMenuItemExit.Size = new Size(180, 22);
            ToolStripMenuItemExit.Text = "終了";
            ToolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;
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
        private ToolStripMenuItem ToolStripMenuItemLoginNav;
        private ToolStripMenuItem ToolStripMenuItemLoginButton;
        private ToolStripMenuItem ToolStripMenuItemCheckUpdate;
        private ToolStripMenuItem ToolStripMenuItemExit;
        private ToolStripMenuItem ToolStripMenuItemLinkList;
        private ToolStripMenuItem ToolStripMenuItemLinkPortal;
        private ToolStripMenuItem ToolStripMenuItemLinkPolite3;
        private ToolStripMenuItem ToolStripMenuItemLinkKyomu;
        private ToolStripMenuItem ToolStripMenuItemLoginStatus;
        private ToolStripMenuItem ログアウトToolStripMenuItem;
    }
}