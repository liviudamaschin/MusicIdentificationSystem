using System;

namespace MusicIdentificationSystem.EF.Entities
{
    public class SpGetUnprocessedStreamsReturnModel
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StationId { get; set; } // StationId
        public DateTime? StartTime { get; set; } // StartTime
        public DateTime? EndTime { get; set; } // EndTime
        public string FileName { get; set; } // FileName (length: 250)
        public string FileNameTransformed { get; set; }
    }
}
