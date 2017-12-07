using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Entities
{
    public class ResultEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StreamId { get; set; } // StreamId
        public int? TrackId { get; set; } // TrackId
        public string Filename { get; set; } // filename (length: 250)
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
