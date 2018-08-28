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

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Period { get; set; }
        public string AccountIds { get; set; }
        public string StreamStationIds { get; set; }
        public string TrackIds { get; set; }

        public int ReportType { get; set; }
        // 1 - fara group by, cu data start melodie(time report details)
        // 2 - cu group by  dupa account, stream station si track(time report first aggregation)
        // 3 - cu group by dupa account(time report maximum aggregation)
        // 4 - count cu group by dupa account, stream station si track(count report first aggregation)
        // 5 - cu group by dupa account(count report maximum aggregation)

        public List<TimeReportModel> TimeReportModelsList { get; set; }
    }
}