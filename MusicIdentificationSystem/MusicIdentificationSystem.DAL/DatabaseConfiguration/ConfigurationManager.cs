using log4net;

namespace MusicIdentificationSystem.DAL.DatabaseConfiguration
{
    public abstract class ConfigurationManager
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(ConfigurationManager));
    }
}
