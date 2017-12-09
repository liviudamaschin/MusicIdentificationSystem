using System;

namespace MusicIdentificationSystem.EF.Entities
{
    public class SpReadTrackByIdReturnModel
    {
        public Int32 Id { get; set; }
        public String ISRC { get; set; }
        public String Artist { get; set; }
        public String Title { get; set; }
        public String Album { get; set; }
        public Int32? ReleaseYear { get; set; }
        public Double? Length { get; set; }
        public Boolean? IsActive { get; set; }
    }
}
