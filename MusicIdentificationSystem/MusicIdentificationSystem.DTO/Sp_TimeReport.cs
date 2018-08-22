using System;

namespace MusicIdentificationSystem.DTO
{
    public class Sp_TimeReport
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int? StreamStationId { get; set; }
        public string StreamStationName { get; set; }
        public int? TrackId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public double? Length { get; set; }
        public int? ResultId { get; set; }
        public decimal? QueryMatchLength { get; set; }
        public decimal? QueryMatchStartsAt { get; set; }
        public int? StreamId { get; set; }
        public string FileName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public decimal? AccountResultsInSeconds { get; set; }
        public long TotalTimeInSeconds { get; set; }
        public decimal? AccountPercent { get; set; }
    }
}