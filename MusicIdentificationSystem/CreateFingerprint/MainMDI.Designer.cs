namespace CreateFingerprint
{
    partial class MainMDI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamStationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TracksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fingerprintFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.adminToolStripMenuItem,
            this.streamToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1282, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamStationsToolStripMenuItem,
            this.TracksMenuItem,
            this.fingerprintFolderToolStripMenuItem,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(44, 24);
            this.FileMenuItem.Text = "&File";
            // 
            // streamStationsToolStripMenuItem
            // 
            this.streamStationsToolStripMenuItem.Name = "streamStationsToolStripMenuItem";
            this.streamStationsToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.streamStationsToolStripMenuItem.Text = "Stream stations";
            this.streamStationsToolStripMenuItem.Click += new System.EventHandler(this.streamStationsToolStripMenuItem_Click);
            // 
            // TracksMenuItem
            // 
            this.TracksMenuItem.Name = "TracksMenuItem";
            this.TracksMenuItem.Size = new System.Drawing.Size(186, 26);
            this.TracksMenuItem.Text = "Tracks";
            this.TracksMenuItem.Click += new System.EventHandler(this.TracksMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(186, 26);
            this.ExitMenuItem.Text = "&Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationSettingsToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.adminToolStripMenuItem.Text = "Admin";
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.applicationSettingsToolStripMenuItem.Text = "Application settings";
            // 
            // streamToolStripMenuItem
            // 
            this.streamToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamListToolStripMenuItem});
            this.streamToolStripMenuItem.Name = "streamToolStripMenuItem";
            this.streamToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.streamToolStripMenuItem.Text = "Stream";
            // 
            // streamListToolStripMenuItem
            // 
            this.streamListToolStripMenuItem.Name = "streamListToolStripMenuItem";
            this.streamListToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.streamListToolStripMenuItem.Text = "Stream list";
            // 
            // fingerprintFolderToolStripMenuItem
            // 
            this.fingerprintFolderToolStripMenuItem.Name = "fingerprintFolderToolStripMenuItem";
            this.fingerprintFolderToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
            this.fingerprintFolderToolStripMenuItem.Text = "Fingerprint folder";
            this.fingerprintFolderToolStripMenuItem.Click += new System.EventHandler(this.fingerprintFolderToolStripMenuItem_Click);
            // 
            // MainMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 867);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMDI";
            this.Text = "MainMDI";
            this.Load += new System.EventHandler(this.MainMDI_Load);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TracksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamStationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fingerprintFolderToolStripMenuItem;
    }
}