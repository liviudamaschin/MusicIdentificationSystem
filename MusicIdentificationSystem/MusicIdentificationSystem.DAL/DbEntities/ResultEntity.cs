
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Result")]
    public class ResultEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("StreamId")]
        public int? StreamId { get; set; } // StreamId
        [Column("IdTrackId")]
        public int? TrackId { get; set; } // TrackId
        [Column("Filename")]
        public string Filename { get; set; } // filename (length: 250)
        [Column("MatchStartAt")]
        public decimal? MatchStartAt { get; set; } // MatchStartAt

        // Foreign keys

        /// <summary>
        /// Parent Stream pointed by [Results].([StreamId]) (FK_TrackResults_Stream)
        /// </summary>
        public virtual StreamEntity Stream { get; set; } // FK_TrackResults_Stream

        /// <summary>
        /// Parent Track pointed by [Results].([TrackId]) (FK_TrackResults_Tracks)
        /// </summary>
        public virtual TrackEntity Track { get; set; } // FK_TrackResults_Tracks

    }
}
