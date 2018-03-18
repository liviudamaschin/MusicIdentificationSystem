using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Tracks")]
    public class TrackEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Isrc { get; set; } // ISRC (length: 50)
        public string Artist { get; set; } // Artist (length: 255)
        public string Title { get; set; } // Title (length: 255)
        public string Album { get; set; } // Album (length: 255)
        public int? ReleaseYear { get; set; } // ReleaseYear
        public double? Length { get; set; } // Length
        public bool IsActive { get; set; } // IsActive

        // Reverse navigation

        /// <summary>
        /// Child Fingerprints where [Fingerprints].[TrackId] point to this entity (FK_Fingerprints_Tracks)
        /// </summary>
        public virtual ICollection<FingerprintEntity> Fingerprints { get; set; } // Fingerprints.FK_Fingerprints_Tracks
        /// <summary>
        /// Child Results where [Results].[TrackId] point to this entity (FK_TrackResults_Tracks)
        /// </summary>
        public virtual ICollection<ResultEntity> Results { get; set; } // Results.FK_TrackResults_Tracks
        /// <summary>
        /// Child SubFingerprints where [SubFingerprints].[TrackId] point to this entity (FK_SubFingerprints_Tracks)
        /// </summary>
        public virtual ICollection<SubFingerprintEntity> SubFingerprints { get; set; } // SubFingerprints.FK_SubFingerprints_Tracks

        public TrackEntity()
        {
            ReleaseYear = 0;
            Length = 0;
            IsActive = true;
            Fingerprints = new List<FingerprintEntity>();
            Results = new List<ResultEntity>();
            SubFingerprints = new List<SubFingerprintEntity>();
        }
    }
}
