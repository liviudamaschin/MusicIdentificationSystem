using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.StreamStationsVideo")]
    public class StreamStationVideoEntity
    {
        [Key]
        public int Id { get; set; } // Id (Primary key)
        public string StationName { get; set; } // StationName (length: 250)
        public string LocalPath { get; set; } // LocalPath
        public bool IsActive { get; set; } // IsActive
        public string Description { get; set; } // Description (length: 250)
        public bool Running { get; set; } // Running
        public string TransformFolder { get; set; } // TransformFolder
        public bool IsConvertionNeeded { get; set; }
        // Reverse navigation

        /// <summary>
        /// Child Streams where [Stream].[StationId] point to this entity (FK_Stream_StreamStations)
        /// </summary>
        //public virtual ICollection<StreamEntity> Streams { get; set; } // Stream.FK_Stream_StreamStations
    }
}
