namespace Url_Quick_Short
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBoxAuthentication = new System.Windows.Forms.GroupBox();
            this.splitContainerAuthentication = new System.Windows.Forms.SplitContainer();
            this.btnTryAuthenticate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxProviders = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openApplicationPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStoragePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkUseCtrlKey = new System.Windows.Forms.CheckBox();
            this.chkUseAltKey = new System.Windows.Forms.CheckBox();
            this.chkUseShiftKey = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxSupportedKeys = new System.Windows.Forms.ComboBox();
            this.btnApplyTriggerKey = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enableKeyLogDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceKeyFlushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxAuthentication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAuthentication)).BeginInit();
            this.splitContainerAuthentication.Panel2.SuspendLayout();
            this.splitContainerAuthentication.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAuthentication
            // 
            this.groupBoxAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAuthentication.Controls.Add(this.splitContainerAuthentication);
            this.groupBoxAuthentication.Location = new System.Drawing.Point(12, 62);
            this.groupBoxAuthentication.Name = "groupBoxAuthentication";
            this.groupBoxAuthentication.Size = new System.Drawing.Size(309, 219);
            this.groupBoxAuthentication.TabIndex = 2;
            this.groupBoxAuthentication.TabStop = false;
            this.groupBoxAuthentication.Text = "authentication";
            // 
            // splitContainerAuthentication
            // 
            this.splitContainerAuthentication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAuthentication.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerAuthentication.IsSplitterFixed = true;
            this.splitContainerAuthentication.Location = new System.Drawing.Point(3, 25);
            this.splitContainerAuthentication.Name = "splitContainerAuthentication";
            this.splitContainerAuthentication.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerAuthentication.Panel2
            // 
            this.splitContainerAuthentication.Panel2.Controls.Add(this.btnTryAuthenticate);
            this.splitContainerAuthentication.Size = new System.Drawing.Size(303, 191);
            this.splitContainerAuthentication.SplitterDistance = 139;
            this.splitContainerAuthentication.TabIndex = 0;
            // 
            // btnTryAuthenticate
            // 
            this.btnTryAuthenticate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTryAuthenticate.Location = new System.Drawing.Point(163, 3);
            this.btnTryAuthenticate.Name = "btnTryAuthenticate";
            this.btnTryAuthenticate.Size = new System.Drawing.Size(137, 43);
            this.btnTryAuthenticate.TabIndex = 0;
            this.btnTryAuthenticate.Text = "try authenticate";
            this.btnTryAuthenticate.UseVisualStyleBackColor = true;
            this.btnTryAuthenticate.Click += new System.EventHandler(this.btnTryAuthenticate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Provider";
            // 
            // cbxProviders
            // 
            this.cbxProviders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProviders.FormattingEnabled = true;
            this.cbxProviders.Location = new System.Drawing.Point(84, 27);
            this.cbxProviders.Name = "cbxProviders";
            this.cbxProviders.Size = new System.Drawing.Size(237, 29);
            this.cbxProviders.TabIndex = 1;
            this.cbxProviders.SelectedIndexChanged += new System.EventHandler(this.cbxProviders_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(567, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openApplicationPathToolStripMenuItem,
            this.openStoragePathToolStripMenuItem,
            this.enableKeyLogDebugToolStripMenuItem,
            this.forceKeyFlushToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openApplicationPathToolStripMenuItem
            // 
            this.openApplicationPathToolStripMenuItem.Name = "openApplicationPathToolStripMenuItem";
            this.openApplicationPathToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openApplicationPathToolStripMenuItem.Text = "Open &Application Path";
            this.openApplicationPathToolStripMenuItem.Click += new System.EventHandler(this.openApplicationPathToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openStoragePathToolStripMenuItem
            // 
            this.openStoragePathToolStripMenuItem.Name = "openStoragePathToolStripMenuItem";
            this.openStoragePathToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openStoragePathToolStripMenuItem.Text = "Open &Storage Path";
            this.openStoragePathToolStripMenuItem.Click += new System.EventHandler(this.openStoragePathToolStripMenuItem_Click);
            // 
            // chkUseCtrlKey
            // 
            this.chkUseCtrlKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseCtrlKey.AutoSize = true;
            this.chkUseCtrlKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseCtrlKey.Location = new System.Drawing.Point(172, 28);
            this.chkUseCtrlKey.Name = "chkUseCtrlKey";
            this.chkUseCtrlKey.Size = new System.Drawing.Size(50, 25);
            this.chkUseCtrlKey.TabIndex = 4;
            this.chkUseCtrlKey.Text = "ctrl";
            this.chkUseCtrlKey.UseVisualStyleBackColor = false;
            // 
            // chkUseAltKey
            // 
            this.chkUseAltKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseAltKey.AutoSize = true;
            this.chkUseAltKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseAltKey.Enabled = false;
            this.chkUseAltKey.Location = new System.Drawing.Point(176, 59);
            this.chkUseAltKey.Name = "chkUseAltKey";
            this.chkUseAltKey.Size = new System.Drawing.Size(46, 25);
            this.chkUseAltKey.TabIndex = 5;
            this.chkUseAltKey.Text = "alt";
            this.chkUseAltKey.UseVisualStyleBackColor = false;
            // 
            // chkUseShiftKey
            // 
            this.chkUseShiftKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseShiftKey.AutoSize = true;
            this.chkUseShiftKey.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseShiftKey.Location = new System.Drawing.Point(163, 90);
            this.chkUseShiftKey.Name = "chkUseShiftKey";
            this.chkUseShiftKey.Size = new System.Drawing.Size(59, 25);
            this.chkUseShiftKey.TabIndex = 6;
            this.chkUseShiftKey.Text = "shift";
            this.chkUseShiftKey.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnApplyTriggerKey);
            this.groupBox1.Controls.Add(this.cbxSupportedKeys);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkUseCtrlKey);
            this.groupBox1.Controls.Add(this.chkUseShiftKey);
            this.groupBox1.Controls.Add(this.chkUseAltKey);
            this.groupBox1.Location = new System.Drawing.Point(327, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 254);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "trigger";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "key";
            // 
            // cbxSupportedKeys
            // 
            this.cbxSupportedKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSupportedKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSupportedKeys.FormattingEnabled = true;
            this.cbxSupportedKeys.Location = new System.Drawing.Point(6, 142);
            this.cbxSupportedKeys.Name = "cbxSupportedKeys";
            this.cbxSupportedKeys.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbxSupportedKeys.Size = new System.Drawing.Size(216, 29);
            this.cbxSupportedKeys.TabIndex = 8;
            // 
            // btnApplyTriggerKey
            // 
            this.btnApplyTriggerKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyTriggerKey.Location = new System.Drawing.Point(85, 177);
            this.btnApplyTriggerKey.Name = "btnApplyTriggerKey";
            this.btnApplyTriggerKey.Size = new System.Drawing.Size(137, 38);
            this.btnApplyTriggerKey.TabIndex = 1;
            this.btnApplyTriggerKey.Text = "apply";
            this.btnApplyTriggerKey.UseVisualStyleBackColor = true;
            this.btnApplyTriggerKey.Click += new System.EventHandler(this.btnApplyTriggerKey_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipTitle = "Url Quick Short";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Url Quick Short";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem1.Text = "E&xit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // enableKeyLogDebugToolStripMenuItem
            // 
            this.enableKeyLogDebugToolStripMenuItem.Name = "enableKeyLogDebugToolStripMenuItem";
            this.enableKeyLogDebugToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.enableKeyLogDebugToolStripMenuItem.Text = "&Enable Key Log (Debug)";
            this.enableKeyLogDebugToolStripMenuItem.Click += new System.EventHandler(this.enableKeyLogDebugToolStripMenuItem_Click);
            // 
            // forceKeyFlushToolStripMenuItem
            // 
            this.forceKeyFlushToolStripMenuItem.Name = "forceKeyFlushToolStripMenuItem";
            this.forceKeyFlushToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.forceKeyFlushToolStripMenuItem.Text = "&Force Key Flush";
            this.forceKeyFlushToolStripMenuItem.Click += new System.EventHandler(this.forceKeyFlushToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 293);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbxProviders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxAuthentication);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(583, 332);
            this.Name = "frmMain";
            this.Text = "Url Quick Short - BETA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBoxAuthentication.ResumeLayout(false);
            this.splitContainerAuthentication.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAuthentication)).EndInit();
            this.splitContainerAuthentication.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAuthentication;
        private System.Windows.Forms.SplitContainer splitContainerAuthentication;
        private System.Windows.Forms.Button btnTryAuthenticate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxProviders;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openApplicationPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStoragePathToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkUseCtrlKey;
        private System.Windows.Forms.CheckBox chkUseAltKey;
        private System.Windows.Forms.CheckBox chkUseShiftKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApplyTriggerKey;
        private System.Windows.Forms.ComboBox cbxSupportedKeys;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enableKeyLogDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceKeyFlushToolStripMenuItem;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

