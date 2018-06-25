
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Results")]
    public class ResultEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("StreamId")]
        public int? StreamId { get; set; } // StreamId
        [Column("TrackId")]
        public int? TrackId { get; set; } // TrackId
        [Column("Filename")]
        public string Filename { get; set; } // filename (length: 250)
        [Column("TrackMatchStartAt")]
        public decimal? TrackMatchStartAt { get; set; } // returns an approximation where does the matched track starts, always relative to the query

        public decimal? QueryMatchStartsAt { get; set; }// returns time position where resulting track started to match in the query
        public decimal? QueryMatchLength { get; set; } // returns how many query seconds matched the resulting track
        public decimal? TrackStartsAt { get; set; } // returns time position where the query started to match in the resulting track
        public decimal? Coverage { get; set; } //returns a value between [0, 1], informing how much the query covered the resulting track (i.e. a 2 minutes query found a 30 seconds track within it, starting at 100th second, coverage will be equal to (120 - 100)/30 ~= 0.66)
        public decimal? Confidence { get; set; } // returns a value between [0, 1]. A value below 0.15 is most probably a false positive. A value bigger than 0.15 is very likely to be an exact match. For good audio quality queries you can expect getting a confidence > 0.5.



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
