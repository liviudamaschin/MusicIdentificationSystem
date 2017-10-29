using MdiHelper;
using MusicIdentificationSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MdiHelper.MdiParent;

namespace CreateFingerprint
{
    public partial class frmTracks : Form
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public frmTracks()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTracks_Load(object sender, EventArgs e)
        {
            try
            {

                var tracks = unitOfWork.TrackRepository.Get();

                //dgvStationList.DataSource = streamStations.ToList();

                metroGrid1.DataSource = tracks.ToList();
            }
            catch (Exception err)
            {
                DevAge.Windows.Forms.ErrorDialog.Show(this, err, "Error loading dataset");
            }
        }

        private void metroGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var id = metroGrid1[0, e.RowIndex].Value.ToString();
            frmEditTrack form = new frmEditTrack(Convert.ToInt32(id));
            form.MdiParent = this.MdiParent;
            ((MdiParent)this.MdiParent).ShowChildDialog(form, ChildDemoForm_DialogReturned);
        }

        private void ChildDemoForm_DialogReturned(object sender, DialogResultArgs e)
        {
            //Reciever of the dialogresult, as specified in the method call (ShowChildDialog) above.
            //MessageBox.Show("ChildDemoForm returned: " + e.Result.ToString());
            int selRow = metroGrid1.SelectedRows[0].Index;
            unitOfWork.DisposeDbContext();
            var traks = unitOfWork.TrackRepository.Get();
            //dgvStationList.DataSource = streamStations.ToList();
            metroGrid1.DataSource = traks.ToList();
            //dgvStationList.Rows[e.RowIndex].Selected = true;
            metroGrid1.Rows[selRow].Selected = true;
        }
    }
}
