namespace CreateFingerprint
{
    partial class frmFingerprintFolder
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtSongsPath = new System.Windows.Forms.TextBox();
            this.btnSelectSongsFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.btnClose = new System.Windows.Forms.Button();
            this.File = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(962, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Process";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSongsPath
            // 
            this.txtSongsPath.Location = new System.Drawing.Point(66, 18);
            this.txtSongsPath.Name = "txtSongsPath";
            this.txtSongsPath.Size = new System.Drawing.Size(927, 22);
            this.txtSongsPath.TabIndex = 4;
            // 
            // btnSelectSongsFolder
            // 
            this.btnSelectSongsFolder.Location = new System.Drawing.Point(999, 16);
            this.btnSelectSongsFolder.Name = "btnSelectSongsFolder";
            this.btnSelectSongsFolder.Size = new System.Drawing.Size(37, 21);
            this.btnSelectSongsFolder.TabIndex = 3;
            this.btnSelectSongsFolder.Text = "...";
            this.btnSelectSongsFolder.UseVisualStyleBackColor = true;
            this.btnSelectSongsFolder.Click += new System.EventHandler(this.btnSelectSongsFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Folder";
            // 
            // metroListView1
            // 
            this.metroListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.File,
            this.Status});
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.GridLines = true;
            this.metroListView1.Location = new System.Drawing.Point(15, 85);
            this.metroListView1.MultiSelect = false;
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(1021, 276);
            this.metroListView1.TabIndex = 7;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            this.metroListView1.View = System.Windows.Forms.View.Details;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(969, 378);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // File
            // 
            this.File.Text = "File";
            this.File.Width = 853;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 123;
            // 
            // frmFingerprintFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1056, 413);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.metroListView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSongsPath);
            this.Controls.Add(this.btnSelectSongsFolder);
            this.Name = "frmFingerprintFolder";
            this.Text = "CreateFingerprint";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSongsPath;
        private System.Windows.Forms.Button btnSelectSongsFolder;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroListView metroListView1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader File;
        private System.Windows.Forms.ColumnHeader Status;
    }
}