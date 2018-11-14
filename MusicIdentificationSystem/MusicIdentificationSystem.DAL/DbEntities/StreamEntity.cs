using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Stream")]
    public class StreamEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("StationId")]
        public int? StationId { get; set; } // StationId
        //[Column("StartTime")]
        public DateTime? StartTime { get; set; } // StartTime
        //[Column("EndTime")]
        public DateTime? EndTime { get; set; } // EndTime
        //[Column("FileName")]
        public string FileName { get; set; } // FileName (length: 250)
        //[Column("FileNameTransformed")]
        public string FileNameTransformed { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string Status { get; set; }
        // Reverse navigation

        /// <summary>
        /// Child Results where [Results].[StreamId] point to this entity (FK_TrackResults_Stream)
        /// </summary>
        //public virtual ICollection<ResultEntity> Results { get; set; } // Results.FK_TrackResults_Stream

        // Foreign keys

        /// <summary>
        /// Parent StreamStation pointed by [Stream].([StationId]) (FK_Stream_StreamStations)
        /// </summary>
        //public virtual ICollection<StreamStationEntity> StreamStation { get; set; } // FK_Stream_StreamStations
    }
}
