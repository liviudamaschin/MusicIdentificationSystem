using MusicIdentificationSystem.DAL.DatabaseConfiguration;
using MusicIdentificationSystem.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        //public DatabaseContext(IDatabaseConfigurationManager databaseConfigurationManager)
        //    : base(databaseConfigurationManager.DbConnectionString)
        //{
        //    ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 30;
        //}

        static DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(null);
        }

        public DatabaseContext()
            : base("Name=FingerprintConnectionString")
        {

        }

        #region Db Sets
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<ApplicationSettingEntity> ApplicationSettings { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<FingerprintEntity> Fingerprints { get; set; }
        public DbSet<ResultEntity> Results { get; set; }
        public DbSet<StreamEntity> Streams { get; set; }
        //public DbSet<StreamResultsEntity> StreamResults { get; set; }
        public DbSet<StreamStationEntity> StreamStations { get; set; }
        public DbSet<SubFingerprintEntity> SubFingerprints { get; set; }
        public DbSet<TrackEntity> Tracks { get; set; }
        public DbSet<AccountXTrackEntity> AccountXTracks { get; set; }
        public DbSet<StreamStationXTrackEntity> StreamStationXTracks { get; set; }
        public DbSet<StreamStationVideoEntity> StreamStationVideo { get; set; }
        public DbSet<StreamVideoEntity> StreamVideo { get; set; }
        public static implicit operator Func<object>(DatabaseContext v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
