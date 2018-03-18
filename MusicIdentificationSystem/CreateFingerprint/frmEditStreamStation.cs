using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Windows.Forms;

namespace CreateFingerprint
{
    public partial class frmEditStreamStation : Form
    {
        private int? streamStationId ;
        //UnitOfWork2 unitOfWork = new UnitOfWork2();
        StreamStationRepository streamStationRepository = new StreamStationRepository();

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
                entity = streamStationRepository.GetByID(this.streamStationId.Value);
                streamStationRepository.Update(entity);
            }
            else
            {
                entity = new StreamStationEntity();
                streamStationRepository.Insert(entity);
            }
            entity.Description = rtbDescription.Text.ToString();
            entity.StationName = txtStationName.Text;
            entity.LocalPath = txtLocalPath.Text;
            entity.IsActive = chkActive.Checked;
            entity.Url = txtUrl.Text;


            streamStationRepository.Save();
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
                var entity = streamStationRepository.GetByID(this.streamStationId.Value);

                rtbDescription.Text = entity.Description;
                txtStationName.Text = entity.StationName;
                txtLocalPath.Text = entity.LocalPath;
                chkActive.Checked = entity.IsActive;
                txtUrl.Text=entity.Url;
            }

        }
    }
}