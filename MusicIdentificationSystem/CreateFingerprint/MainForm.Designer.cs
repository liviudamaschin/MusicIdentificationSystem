namespace CreateFingerprint
{
    partial class MainForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnCreateFingerprint = new System.Windows.Forms.Button();
            this.btnMatchFingerprint = new System.Windows.Forms.Button();
            this.listBoxTracks = new System.Windows.Forms.ListBox();
            this.bindingSourceTracks = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTracks)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "\"All Supported Audio | *.mp3; *.wma; *.wav; | MP3s | *.mp3 | WMAs | *.wma | WAVs " +
    "| *.wav\";";
            this.openFileDialog.InitialDirectory = "T:\\WORK\\Music";
            // 
            // btnCreateFingerprint
            // 
            this.btnCreateFingerprint.Location = new System.Drawing.Point(31, 26);
            this.btnCreateFingerprint.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateFingerprint.Name = "btnCreateFingerprint";
            this.btnCreateFingerprint.Size = new System.Drawing.Size(208, 28);
            this.btnCreateFingerprint.TabIndex = 0;
            this.btnCreateFingerprint.Text = "Create Fingerprint";
            this.btnCreateFingerprint.UseVisualStyleBackColor = true;
            this.btnCreateFingerprint.Click += new System.EventHandler(this.btnCreateFingerprint_Click);
            // 
            // btnMatchFingerprint
            // 
            this.btnMatchFingerprint.Location = new System.Drawing.Point(31, 84);
            this.btnMatchFingerprint.Margin = new System.Windows.Forms.Padding(4);
            this.btnMatchFingerprint.Name = "btnMatchFingerprint";
            this.btnMatchFingerprint.Size = new System.Drawing.Size(208, 28);
            this.btnMatchFingerprint.TabIndex = 2;
            this.btnMatchFingerprint.Text = "Match Song";
            this.btnMatchFingerprint.UseVisualStyleBackColor = true;
            this.btnMatchFingerprint.Click += new System.EventHandler(this.btnMatchFingerprint_Click);
            // 
            // listBoxTracks
            // 
            this.listBoxTracks.DataSource = this.bindingSourceTracks;
            this.listBoxTracks.DisplayMember = "Display";
            this.listBoxTracks.FormattingEnabled = true;
            this.listBoxTracks.ItemHeight = 16;
            this.listBoxTracks.Location = new System.Drawing.Point(31, 144);
            this.listBoxTracks.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxTracks.Name = "listBoxTracks";
            this.listBoxTracks.ScrollAlwaysVisible = true;
            this.listBoxTracks.Size = new System.Drawing.Size(853, 404);
            this.listBoxTracks.TabIndex = 3;
            this.listBoxTracks.ValueMember = "Value";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(495, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "mp3 to wav";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 559);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(459, 22);
            this.textBox1.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(562, 559);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(562, 587);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(97, 587);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(459, 22);
            this.textBox2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 565);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 590);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Destination";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Source";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(623, 31);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(42, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(333, 32);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(284, 22);
            this.textBox3.TabIndex = 11;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(539, 60);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(126, 27);
            this.button5.TabIndex = 14;
            this.button5.Text = "Match songs";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 665);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxTracks);
            this.Controls.Add(this.btnMatchFingerprint);
            this.Controls.Add(this.btnCreateFingerprint);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Music Fingerprint";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTracks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnCreateFingerprint;
        private System.Windows.Forms.Button btnMatchFingerprint;
        private System.Windows.Forms.ListBox listBoxTracks;
        private System.Windows.Forms.BindingSource bindingSourceTracks;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button5;
    }
}

