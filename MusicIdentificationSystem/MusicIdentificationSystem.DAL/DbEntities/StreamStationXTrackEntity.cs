using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.StreamStationXTracks")]
    public class StreamStationXTrackEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("StreamStationId")]
        public int StreamStationId { get; set; } // StreamStationId
        [Column("TrackId")]
        public int TrackId { get; set; } // TrackId
        [Column("IsActive")]
        public bool IsActive { get; set; } // IsActive

        // Foreign keys

        public virtual StreamStationEntity StreamStation { get; set; } // FK_StreamStationXTracks_StreamStations

        public virtual TrackEntity Track { get; set; } // FK_StreamStationXTracks_Tracks
    }
}
