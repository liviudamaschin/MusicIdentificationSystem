using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Entities
{
    public class StreamResultsEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public int StreamId { get; set; } // StreamId
        public int ResultId { get; set; } // ResultId
    }
}
