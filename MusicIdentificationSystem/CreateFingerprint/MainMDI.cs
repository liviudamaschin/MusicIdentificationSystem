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
    public partial class MainMDI : Form
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
            CreateNewFingerprint createNewFingerprint = new CreateNewFingerprint();
            createNewFingerprint.MdiParent = this;
            createNewFingerprint.Show();
        }

        private void newStreamStationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewStreamStation newForm = new NewStreamStation();
            newForm.MdiParent = this;
            newForm.Show();
            
        }

        private void streamStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamStationList newForm = new StreamStationList();
            newForm.MdiParent = this;
            newForm.Show();
        }
    }
}
