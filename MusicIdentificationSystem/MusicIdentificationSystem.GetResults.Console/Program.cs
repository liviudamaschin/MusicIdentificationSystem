
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace MusicIdentificationSystem.GetResults
{
    class Program
    {
        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            //MatchResults.MatchStream();
            MatchResults.MatchResultsFromStream();
        }
    }
}
