using System;

namespace MusicIdentificationSystem.DTO
{
    public class UnprocessedStreams
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FileName { get; set; }
        public string FileNameTransformed { get; set; }
        public bool IsConvertionNeeded { get; set; }
        public string TransformFolder { get; set; }
    }
}
