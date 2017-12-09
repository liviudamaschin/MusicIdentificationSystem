using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Entities
{
    public class ApplicationSettingEntity
    {
        public int Id { get; set; } // Id (Primary key)
        public string KeyName { get; set; } // KeyName (length: 250)
        public string KeyValue { get; set; } // KeyValue (length: 250)
    }
}
