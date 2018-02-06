using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicIdentificationSystem.DAL.DbEntities
{
    [Table("dbo.Fingerprint")]
    public class FingerprintEntity
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; } // Id (Primary key)
        [Column("Signature")]
        public byte[] Signature { get; set; } // Signature (length: 4096)
        [Column("TrackId")]
        public int TrackId { get; set; } // TrackId

        // Foreign keys

        /// <summary>
        /// Parent Track pointed by [Fingerprints].([TrackId]) (FK_Fingerprints_Tracks)
        /// </summary>
        public virtual TrackEntity Track { get; set; } // FK_Fingerprints_Tracks
    }
}
