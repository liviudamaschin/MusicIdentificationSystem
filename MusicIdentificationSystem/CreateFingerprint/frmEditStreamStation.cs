using MetroFramework.Forms;
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
    public partial class frmEditStreamStation : Form
    {
        private int? streamStationId ;
        UnitOfWork unitOfWork = new UnitOfWork();


        public frmEditStreamStation()
        {
            InitializeComponent();
        }
        public frmEditStreamStation(int streamStationId)
        {
            InitializeComponent();
            this.streamStationId = streamStationId;
            InitCustom();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            StreamStationEntity entity = null;
            if (this.streamStationId.HasValue)
            {
                entity = unitOfWork.StreamStationRepository.GetByID(this.streamStationId.Value);
                unitOfWork.StreamStationRepository.Update(entity);
            }
            else
            {
                entity = new StreamStationEntity();
                unitOfWork.StreamStationRepository.Insert(entity);
            }
            entity.Description = rtbDescription.Text.ToString();
            entity.StationName = txtStationName.Text;
            entity.LocalPath = txtLocalPath.Text;
            entity.IsActive = chkActive.Checked;
            entity.Url = txtUrl.Text;


            unitOfWork.Save();
            //StreamStation.AddStreamStation(entity);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InitCustom()
        {
            if (this.streamStationId.HasValue)
            {
                var entity = unitOfWork.StreamStationRepository.GetByID(this.streamStationId.Value);

                rtbDescription.Text = entity.Description;
                txtStationName.Text = entity.StationName;
                txtLocalPath.Text = entity.LocalPath;
                chkActive.Checked = entity.IsActive?? entity.IsActive.Value;
                txtUrl.Text=entity.Url;
            }

        }
    }
}