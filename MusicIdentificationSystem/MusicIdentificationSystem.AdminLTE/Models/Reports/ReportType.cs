using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Reports
{
    public enum ReportType
    {
        TimeReportDetails = 1,
        TimeReportFirstAggregation = 2,
        TimeReportMaximumAggregation = 3,
        CountReportFirstAggregation = 4,
        CountReportMaximumAggregation = 5,
        // 1 - fara group by, cu data start melodie(time report details)
        // 2 - cu group by  dupa account, stream station si track(time report first aggregation)
        // 3 - cu group by dupa account(time report maximum aggregation)
        // 4 - count cu group by dupa account, stream station si track(count report first aggregation)
        // 5 - cu group by dupa account(count report maximum aggregation)
    }
}