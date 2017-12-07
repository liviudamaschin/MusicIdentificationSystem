namespace MusicIdentificationSystem.EF.Entities
{
    public class FingerprintEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public byte[] Signature { get; set; } // Signature (length: 4096)
        public int TrackId { get; set; } // TrackId

        // Foreign keys

        /// <summary>
        /// Parent Track pointed by [Fingerprints].([TrackId]) (FK_Fingerprints_Tracks)
        /// </summary>
        public virtual TrackEntity Track { get; set; } // FK_Fingerprints_Tracks
    }
}
