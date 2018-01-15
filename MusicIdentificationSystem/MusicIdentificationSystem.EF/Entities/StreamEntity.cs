using System;

namespace MusicIdentificationSystem.EF.Entities
{
    public class StreamEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StationId { get; set; } // StationId
        public DateTime? StartTime { get; set; } // StartTime
        public DateTime? EndTime { get; set; } // EndTime
        public string FileName { get; set; } // FileName (length: 250)
        public string FileNameTransformed { get; set; }
        // Reverse navigation

        /// <summary>
        /// Child Results where [Results].[StreamId] point to this entity (FK_TrackResults_Stream)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ResultEntity> Results { get; set; } // Results.FK_TrackResults_Stream

        // Foreign keys

        /// <summary>
        /// Parent StreamStation pointed by [Stream].([StationId]) (FK_Stream_StreamStations)
        /// </summary>
        public virtual StreamStationEntity StreamStation { get; set; } // FK_Stream_StreamStations
    }
}
