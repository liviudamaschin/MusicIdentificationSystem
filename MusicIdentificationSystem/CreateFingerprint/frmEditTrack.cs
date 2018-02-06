using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
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
    public partial class frmEditTrack : Form
    {
        private int? trackId;
        //UnitOfWork2 unitOfWork = new UnitOfWork2();
        TrackRepository trackRepository = new TrackRepository();
        public frmEditTrack()
        {
            InitializeComponent();
        }

        public frmEditTrack(int trackId)
        {
            InitializeComponent();
            this.trackId = trackId;
            InitCustom();
        }

        private void InitCustom()
        {
            if (this.trackId.HasValue)
            {
                var entity = trackRepository.GetByID(this.trackId.Value);

                txtArtist.Text = entity.Artist;
                txtTitle.Text = entity.Title;
                txtAlbum.Text = entity.Album;
                txtReleaseYear.Text = entity.ReleaseYear.Value.ToString();
                txtLength.Text = entity.Length.Value.ToString();
                chkActive.Checked = entity.IsActive.HasValue? entity.IsActive.Value:false;

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TrackEntity entity = null;
            if (this.trackId.HasValue)
            {
                entity = trackRepository.GetByID(this.trackId.Value);
                trackRepository.Update(entity);
            }
            else
            {
                entity = new TrackEntity();
                trackRepository.Insert(entity);
            }
            entity.Album = txtAlbum.Text;
            entity.Title = txtTitle.Text;
            entity.Artist = txtArtist.Text;
            entity.ReleaseYear = Convert.ToInt32(txtReleaseYear.Text);
            entity.Length = Convert.ToDouble(txtLength.Text);
            entity.IsActive = chkActive.Checked;
            


            trackRepository.Save();//StreamStation.AddStreamStation(entity);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
