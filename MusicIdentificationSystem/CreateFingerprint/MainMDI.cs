using MdiHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateFingerprint
{
    public partial class MainMDI : MdiParent
    {
        public MainMDI()
        {
            InitializeComponent();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            frmFingerprintFolder createNewFingerprint = new frmFingerprintFolder();
            createNewFingerprint.MdiParent = this;
            createNewFingerprint.Show();
        }

        private void newStreamStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditStreamStation newForm = new frmEditStreamStation();
            newForm.MdiParent = this;
            newForm.Show();
            
        }

        private void streamStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStationList newForm = new frmStationList();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void MainMDI_Load(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Left = 0;
            this.Top = 0;
        }

        private void TracksMenuItem_Click(object sender, EventArgs e)
        {
            frmTracks newForm = new frmTracks();
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void fingerprintFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFingerprintFolder newForm = new frmFingerprintFolder();
            newForm.MdiParent = this;
            newForm.Show();
        }
        //CreateNewFingerprint createNewFingerprint = new CreateNewFingerprint();
        //createNewFingerprint.MdiParent = this;
        //createNewFingerprint.Show();
    }
}
