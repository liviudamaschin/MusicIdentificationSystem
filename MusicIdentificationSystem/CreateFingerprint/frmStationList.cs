using MdiHelper;
using MetroFramework.Forms;
using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.EF.Entities;
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
    public partial class frmStationList : Form
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public frmStationList()
        {
            InitializeComponent();
        }
       
        //Controllers
        //private SourceGrid.Cells.Controllers.Button mController_Link;

        private void StationList_Load(object sender, EventArgs e)
        {

            try
            {

                var streamStations = unitOfWork.StreamStationRepository.Get();

                //dgvStationList.DataSource = streamStations.ToList();

                metroGrid1.DataSource = streamStations.ToList();
            }
            catch (Exception err)
            {
                DevAge.Windows.Forms.ErrorDialog.Show(this, err, "Error loading dataset");
            }
            
        }

        //private void dgvStationList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var id = dgvStationList[0, e.RowIndex].Value.ToString();
        //    NewStreamStation form = new NewStreamStation(Convert.ToInt32(id));
        //    form.MdiParent = this.MdiParent;
        //    ((MdiParent)this.MdiParent).ShowChildDialog(form, ChildDemoForm_DialogReturned);
        //    //var streamStations = unitOfWork.StreamStationRepository.Get();

        //    //dgvStationList.DataSource = streamStations.ToList();
        //    //dgvStationList.Rows[e.RowIndex].Selected = true;

        //}
        private void ChildDemoForm_DialogReturned(object sender, DialogResultArgs e)
        {
            //Reciever of the dialogresult, as specified in the method call (ShowChildDialog) above.
            //MessageBox.Show("ChildDemoForm returned: " + e.Result.ToString());
            int selRow=metroGrid1.SelectedRows[0].Index;
            unitOfWork.DisposeDbContext();
            var streamStations = unitOfWork.StreamStationRepository.Get();
            //dgvStationList.DataSource = streamStations.ToList();
            metroGrid1.DataSource = streamStations.ToList();
            //dgvStationList.Rows[e.RowIndex].Selected = true;
            metroGrid1.Rows[selRow].Selected = true;
        }

        private void metroGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var id = metroGrid1[0, e.RowIndex].Value.ToString();
            frmEditStreamStation form = new frmEditStreamStation(Convert.ToInt32(id));
            form.MdiParent = this.MdiParent;
            ((MdiParent)this.MdiParent).ShowChildDialog(form, ChildDemoForm_DialogReturned);
        }

     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            
            frmEditStreamStation form = new frmEditStreamStation();
            form.MdiParent = this.MdiParent;
            ((MdiParent)this.MdiParent).ShowChildDialog(form, ChildDemoForm_DialogReturned);
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
