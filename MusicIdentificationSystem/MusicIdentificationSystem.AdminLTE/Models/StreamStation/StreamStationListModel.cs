using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.StreamStation
{
    public class StreamStationListModel
    {
        public StreamStationListModel()
        {
            StreamStationModelsList = new List<StreamStationModel>();
        }

        public List<StreamStationModel> StreamStationModelsList { get; set; }
    }
}