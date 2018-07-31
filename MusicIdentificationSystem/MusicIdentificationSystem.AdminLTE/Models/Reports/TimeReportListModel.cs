using MusicIdentificationSystem.AdminLTE.Models.Account;
using MusicIdentificationSystem.AdminLTE.Models.StreamStation;
using MusicIdentificationSystem.AdminLTE.Models.Track;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Reports
{
    public class TimeReportListModel
    {
        public TimeReportListModel()
        {
            TimeReportModelsList = new List<TimeReportModel>();
        }

        public string Period { get; set; }
        public string AccountIds { get; set; }
        public string StreamStationIds { get; set; }
        public string TrackIds { get; set; }

        public List<TimeReportModel> TimeReportModelsList { get; set; }
    }
}