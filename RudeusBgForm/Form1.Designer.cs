using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;

namespace RudeusBgForm
{
    partial class Form1

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
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { testMessageToolStripMenuItem, testMassageToolStripMenuItem, tastSausageToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(166, 76);
            // 
            // testMessageToolStripMenuItem
            // 
            testMessageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { testMessageToolStripMenuItem1 });
            testMessageToolStripMenuItem.Name = "testMessageToolStripMenuItem";
            testMessageToolStripMenuItem.Size = new Size(165, 24);
            testMessageToolStripMenuItem.Text = "Test message";
            // 
            // testMessageToolStripMenuItem1
            // 
            testMessageToolStripMenuItem1.Name = "testMessageToolStripMenuItem1";
            testMessageToolStripMenuItem1.Size = new Size(224, 26);
            testMessageToolStripMenuItem1.Text = "Test message";
            // 
            // testMassageToolStripMenuItem
            // 
            testMassageToolStripMenuItem.Name = "testMassageToolStripMenuItem";
            testMassageToolStripMenuItem.Size = new Size(165, 24);
            testMassageToolStripMenuItem.Text = "Test massage";
            // 
            // tastSausageToolStripMenuItem
            // 
            tastSausageToolStripMenuItem.Name = "tastSausageToolStripMenuItem";
            tastSausageToolStripMenuItem.Size = new Size(210, 24);
            tastSausageToolStripMenuItem.Text = "Tast sausage";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Form1";
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