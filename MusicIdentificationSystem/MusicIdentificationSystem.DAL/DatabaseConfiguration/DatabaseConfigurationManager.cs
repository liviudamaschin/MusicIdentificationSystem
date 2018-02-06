using System;

namespace MusicIdentificationSystem.DAL.DatabaseConfiguration
{
    public class DatabaseConfigurationManager : ConfigurationManager, IDatabaseConfigurationManager
    {
        public string DbConnectionString => throw new NotImplementedException();

        public string DbConnectionTimeout => throw new NotImplementedException();

        int IDatabaseConfigurationManager.DbConnectionTimeout => throw new NotImplementedException();
    }
}
