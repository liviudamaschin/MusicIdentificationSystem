using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Track
{
    public class TrackListModel
    {
        public TrackListModel()
        {
            TrackModelsList = new List<TrackModel>();
        }

        public List<TrackModel> TrackModelsList { get; set; }
    }
}