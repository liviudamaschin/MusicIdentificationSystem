using System;
namespace MusicIdentificationSystem.EF.Entities
{
    public class SpReadFingerprintByTrackIdReturnModel
    {
        public int Id { get; set; }
        public Byte[] Signature { get; set; }
        public int TrackId { get; set; }
    }
}
