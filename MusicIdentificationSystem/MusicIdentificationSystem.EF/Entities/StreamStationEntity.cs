namespace MusicIdentificationSystem.EF.Entities
{
    public class StreamStationEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string StationName { get; set; } // StationName (length: 250)
        public string Url { get; set; } // URL
        public string LocalPath { get; set; } // LocalPath
        public bool? IsActive { get; set; } // IsActive
        public string Description { get; set; } // Description (length: 250)

        // Reverse navigation

        /// <summary>
        /// Child Streams where [Stream].[StationId] point to this entity (FK_Stream_StreamStations)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<StreamEntity> Streams { get; set; } // Stream.FK_Stream_StreamStations

    }
}
