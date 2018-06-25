using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.Dashboard
{
    public class StreamStationStatusModel
    {
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_Id")]
        public int Id { get; set; } // Id (Primary key)
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_StationName")]
        public string StationName { get; set; } // StreamStationName (length: 50)
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_Url")]
        public string Url { get; set; } // URL
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_Description")]
        public string Description { get; set; } // Description (length: 250)
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_IsActive")]
        public bool IsActive { get; set; } // IsActive
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_Running")]
        public bool Running { get; set; } // Running
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_LocalPath")]
        public string LocalPath { get; set; } // LocalPath
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_TransformFolder")]
        public string TransformFolder { get; set; } // TransformFolder
        [Display(ResourceType = typeof(Resources.Resources), Name = "StreamStationStatus_IsConvertionNeeded")]
        public bool IsConvertionNeeded { get; set; }
    }
}