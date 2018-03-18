using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Track
{
    public class TrackModel
    {
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_Id")]
        public int Id { get; set; } // Id (Primary key)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_Isrc")]
        public string Isrc { get; set; } // ISRC (length: 50)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_Artist")]
        public string Artist { get; set; } // Artist (length: 255)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_Title")]
        public string Title { get; set; } // Title (length: 255)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_Album")]
        public string Album { get; set; } // Album (length: 255)
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_ReleaseYear")]
        public int? ReleaseYear { get; set; } // ReleaseYear
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_Length")]
        public double? Length { get; set; } // Length
        public string LengthText { get; set; } // Length
        [Display(ResourceType = typeof(Resources.Resources), Name = "Track_IsActive")]
        public bool IsActive { get; set; } // IsActive
    }
}