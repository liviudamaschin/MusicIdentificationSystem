using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.DatabaseConfiguration
{
    public interface IDatabaseConfigurationManager 
    {
        string DbConnectionString { get; }
        int DbConnectionTimeout { get; }
    }
}
