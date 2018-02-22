﻿using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;

namespace MusicIdentificationSystem.Common
{
    public class cConfig
    {
        public Dictionary<string, string> AppSettings = new Dictionary<string, string>();

        //private UnitOfWork2 unitOfWork = new UnitOfWork2();
        ApplicationSettingRepository applicationSettingRepository = new ApplicationSettingRepository();
        public cConfig()
        {
            foreach (var item in applicationSettingRepository.GetList())
            {
                AppSettings.Add(item.KeyName, item.KeyValue);
            }
        }

    }

    public class cApp
    {
        // This is the only time System.Configuration.ConfigurationManager.AppSettings is called.
        // The appSetting ApplicationEnv is in machine.config and will be one of the values “Dev”, “Test”, “QA”, “Prod” or “DR” 
        static readonly cConfig _config = new cConfig();

        public static Dictionary<string, string> AppSettings
        {
            get
            {
                return _config.AppSettings;
            }
        }
    }
}