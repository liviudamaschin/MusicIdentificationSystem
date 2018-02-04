using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DTO
{
    public class ActiveStation
    {
        public int Id { get; set; }
        public string StationName { get; set; }
        public string Url { get; set; }
        public string LocalPath { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public bool Running { get; set; }
        public string TransformFolder { get; set; }
        public string FileName { get; set; }

    }
}
