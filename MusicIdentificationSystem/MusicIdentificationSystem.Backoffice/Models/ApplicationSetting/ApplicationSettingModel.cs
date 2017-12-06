using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.Backoffice.Models.ApplicationSetting
{
    public class ApplicationSettingModel
    {
        //[Display(Name = "Id")]
        //public int Id { get; set; }

        //[Display( Name = "Companie")]
        //public int? CompanyId { get; set; }

        //[Display( Name = "Nume")]
        //public string Name { get; set; }

        //[Display(Name = "Valoare")]
        //public string Value { get; set; }

        public ApplicationSettingModel()
        {
        }

        [Display(Name = "Id")]
        public int Id { get; set; } // Id (Primary key)

        [Display(Name = "Nume")]
        public string KeyName { get; set; } // KeyName (length: 250)

        [Display(Name = "Valoare")]
        public string KeyValue { get; set; } // KeyValue (length: 250)
    }
}