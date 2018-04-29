using MusicIdentificationSystem.AdminLTE.Models.Track;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.CreateFingerprint
{
    public class FingerprintListModel
    {
        public FingerprintListModel()
        {
            FingerprintModelsList = new List<TrackModel>();
        }

        public int AccountId { get; set; }
        public string SongsFolder { get; set; }
        public List<TrackModel> FingerprintModelsList { get; set; }
    }
}