using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.EF.Context;
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

namespace CreateFingerprint
{
    public partial class NewStreamStation : Form
    {
        public NewStreamStation()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var entity = new StreamStationEntity();
            entity.Description = rtbDescription.Text.ToString();
            entity.StationName = txtStationName.Text;
            entity.IsActive = chkActive.Checked;
            entity.Url = txtUrl.Text;

            StreamStation.AddStreamStation(entity);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}