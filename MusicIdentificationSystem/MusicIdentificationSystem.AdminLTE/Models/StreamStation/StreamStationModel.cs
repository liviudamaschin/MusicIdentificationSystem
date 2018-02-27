using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.StreamStation
{
    public class StreamStationModel
    {
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_Id")]
        public int Id { get; set; } // Id (Primary key)
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_StationName")]
        public string StationName { get; set; } // StreamStationName (length: 50)
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_Url")]
        public string Url { get; set; } // URL
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_Description")]
        public string Description { get; set; } // Description (length: 250)
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_IsActive")]
        public bool IsActive { get; set; } // IsActive
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_Running")]
        public bool Running { get; set; } // Running
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_LocalPath")]
        public string LocalPath { get; set; } // LocalPath
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_TransformFolder")]
        public string TransformFolder { get; set; } // TransformFolder
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStation_IsConvertionNeeded")]
        public bool IsConvertionNeeded { get; set; }
    }
}