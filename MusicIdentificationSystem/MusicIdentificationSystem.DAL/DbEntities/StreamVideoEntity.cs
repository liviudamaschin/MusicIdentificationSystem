using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.StreamVideo")]
    public class StreamVideoEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("StreamStationVideoId")]
        public int? StreamStationVideoId { get; set; } // StationId
        //[Column("StartTime")]
        public DateTime? StartTime { get; set; } // StartTime
        //[Column("EndTime")]
        public DateTime? EndTime { get; set; } // EndTime
        //[Column("FileName")]
        public string FileName { get; set; } // FileName (length: 250)
        //[Column("FileNameTransformed")]
        public string FileNameTransformed { get; set; }
        public DateTime? ProcessDate { get; set; }
    }
}
