using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicIdentificationSystem.AdminLTE.Models.ApplicationSetting
{
    public class ApplicationSettingListModel
    {
        public ApplicationSettingListModel()
        {
            ApplicationSettingModelsList = new List<ApplicationSettingModel>();
        }

        public List<ApplicationSettingModel> ApplicationSettingModelsList { get; set; }
    }
}