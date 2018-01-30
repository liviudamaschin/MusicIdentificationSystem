using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicIdentificationSystem.AdminLTE.Models.ApplicationSetting
{
    public class ApplicationSettingModel
    {
        public ApplicationSettingModel()
        {
            AccountsList = new List<SelectListItem>();
        }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_Id")]
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_KeyName")]
        public string KeyName { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_KeyValue")]
        public string KeyValue { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_Description")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_AccountId")]
        public int? AccountId { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_AccountName")]
        public string AccountName { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_IsActive")]
        public bool IsActive { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "ApplicationSetting_AccountsList")]
        //[IgnoreMap]
        public List<SelectListItem> AccountsList { get; set; }
        //public IEnumerable<SelectListItem> AccountsList { get; set; }
    }
}