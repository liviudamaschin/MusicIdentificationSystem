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
    public class TimeReportModel
    {
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_AccountId")]
        public int AccountId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_AccountName")]
        public string AccountName { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_StreamStationId")]
        public int? StreamStationId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_StreamStationName")]
        public string StreamStationName { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_TrackId")]
        public int? TrackId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_TrackTitle")]
        public string Title { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_TrackArtist")]
        public string Artist { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_TrackLength")]
        public double? Length { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_ResultId")]
        public int? ResultId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_QueryMatchLength")]
        public decimal? QueryMatchLength { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_QueryMatchStartsAt")]
        public decimal? QueryMatchStartsAt { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_StreamId")]
        public int? StreamId { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_FileName")]
        public string FileName { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_StartTime")]
        public DateTime? StartTime { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_EndTime")]
        public DateTime? EndTime { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_AccountResultsInSeconds")]
        public decimal? AccountResultsInSeconds { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_TotalTimeInSeconds")]
        public long TotalTimeInSeconds { get; set; }
        [Display(ResourceType = typeof(Resources.Resources), Name = "TimeReport_AccountPercent")]
        public decimal? AccountPercent { get; set; }


        public TimeReportModel()
        {
        }
    }
}