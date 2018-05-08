using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Dashboard
{
    public class StreamStationStatusListModel
    {
        public StreamStationStatusListModel()
        {
            StreamStationStatusModelsList = new List<StreamStationStatusModel>();
        }

        public List<StreamStationStatusModel> StreamStationStatusModelsList { get; set; }
    }
}