using MusicIdentificationSystem.EF.Entities;
using MusicIdentificationSystem.EF.Interfaces;
using MusicIdentificationSystem.EF.Mappings;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Context
{
    public class Db : DbContext, IDb
    {
        public DbSet<AccountEntity> Accounts { get; set; } // Accounts
        public DbSet<AccountStreamStationEntity> AccountStreamStations { get; set; } // AccountStreamStations
        public DbSet<ApplicationSettingEntity> ApplicationSettings { get; set; } // ApplicationSettings
        public DbSet<ClientEntity> Clients { get; set; } // Clients
        public DbSet<FingerprintEntity> Fingerprints { get; set; } // Fingerprints
        public DbSet<ResultEntity> Results { get; set; } // Results
        public DbSet<StreamEntity> Streams { get; set; } // Stream
        public DbSet<StreamStationEntity> StreamStations { get; set; } // StreamStations
        public DbSet<SubFingerprintEntity> SubFingerprints { get; set; } // SubFingerprints
        public DbSet<TrackEntity> Tracks { get; set; } // Tracks

        static Db()
        {
            Database.SetInitializer<Db>(null);
        }

        public Db()
            : base("Name=FingerprintConnectionString")
        {
            
        }

        public Db(string connectionString)
            : base(connectionString)
        {
            
        }

        public Db(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model)
            : base(connectionString, model)
        {
           
        }

        public Db(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
           
        }

        public Db(DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
          
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public bool IsSqlParameterNull(SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AccountEntityMapping());
            modelBuilder.Configurations.Add(new AccountStreamStationEntityMapping());
            modelBuilder.Configurations.Add(new ApplicationSettingEntityMapping());
            modelBuilder.Configurations.Add(new ClientEntityMapping());
            modelBuilder.Configurations.Add(new FingerprintEntityMapping());
            modelBuilder.Configurations.Add(new ResultEntityMapping());
            modelBuilder.Configurations.Add(new StreamEntityMapping());
            modelBuilder.Configurations.Add(new StreamStationEntityMapping());
            modelBuilder.Configurations.Add(new SubFingerprintEntityMapping());
            modelBuilder.Configurations.Add(new TrackEntityMapping());

            base.OnModelCreating(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AccountEntityMapping(schema));
            modelBuilder.Configurations.Add(new AccountStreamStationEntityMapping(schema));
            modelBuilder.Configurations.Add(new ApplicationSettingEntityMapping(schema));
            modelBuilder.Configurations.Add(new ClientEntityMapping(schema));
            modelBuilder.Configurations.Add(new FingerprintEntityMapping(schema));
            modelBuilder.Configurations.Add(new ResultEntityMapping(schema));
            modelBuilder.Configurations.Add(new StreamEntityMapping(schema));
            modelBuilder.Configurations.Add(new StreamStationEntityMapping(schema));
            modelBuilder.Configurations.Add(new SubFingerprintEntityMapping(schema));
            modelBuilder.Configurations.Add(new TrackEntityMapping(schema));
            return modelBuilder;
        }

        // Stored Procedures
        public int SpDeleteTrack(int? id)
        {
            var idParam = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = id.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!id.HasValue)
                idParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "EXEC @procResult = [dbo].[sp_DeleteTrack] @Id", idParam, procResultParam);

            return (int)procResultParam.Value;
        }

        public List<SpInsertFingerprintReturnModel> SpInsertFingerprint(byte[] signature, int? trackId)
        {
            int procResult;
            return SpInsertFingerprint(signature, trackId, out procResult);
        }

        public List<SpInsertFingerprintReturnModel> SpInsertFingerprint(byte[] signature, int? trackId, out int procResult)
        {
            var signatureParam = new SqlParameter { ParameterName = "@Signature", SqlDbType = SqlDbType.VarBinary, Direction = ParameterDirection.Input, Value = signature, Size = 4096 };
            if (signatureParam.Value == null)
                signatureParam.Value = System.DBNull.Value;

            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpInsertFingerprintReturnModel>("EXEC @procResult = [dbo].[sp_InsertFingerprint] @Signature, @TrackId", signatureParam, trackIdParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpInsertFingerprintReturnModel>> SpInsertFingerprintAsync(byte[] signature, int? trackId)
        {
            var signatureParam = new SqlParameter { ParameterName = "@Signature", SqlDbType = SqlDbType.VarBinary, Direction = ParameterDirection.Input, Value = signature, Size = 4096 };
            if (signatureParam.Value == null)
                signatureParam.Value = System.DBNull.Value;

            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpInsertFingerprintReturnModel>("EXEC [dbo].[sp_InsertFingerprint] @Signature, @TrackId", signatureParam, trackIdParam).ToListAsync();

            return procResultData;
        }

        public List<SpInsertSubFingerprintReturnModel> SpInsertSubFingerprint(int? trackId, int? sequenceNumber, double? sequenceAt, long? hashTable0, long? hashTable1, long? hashTable2, long? hashTable3, long? hashTable4, long? hashTable5, long? hashTable6, long? hashTable7, long? hashTable8, long? hashTable9, long? hashTable10, long? hashTable11, long? hashTable12, long? hashTable13, long? hashTable14, long? hashTable15, long? hashTable16, long? hashTable17, long? hashTable18, long? hashTable19, long? hashTable20, long? hashTable21, long? hashTable22, long? hashTable23, long? hashTable24, string clusters)
        {
            int procResult;
            return SpInsertSubFingerprint(trackId, sequenceNumber, sequenceAt, hashTable0, hashTable1, hashTable2, hashTable3, hashTable4, hashTable5, hashTable6, hashTable7, hashTable8, hashTable9, hashTable10, hashTable11, hashTable12, hashTable13, hashTable14, hashTable15, hashTable16, hashTable17, hashTable18, hashTable19, hashTable20, hashTable21, hashTable22, hashTable23, hashTable24, clusters, out procResult);
        }

        public List<SpInsertSubFingerprintReturnModel> SpInsertSubFingerprint(int? trackId, int? sequenceNumber, double? sequenceAt, long? hashTable0, long? hashTable1, long? hashTable2, long? hashTable3, long? hashTable4, long? hashTable5, long? hashTable6, long? hashTable7, long? hashTable8, long? hashTable9, long? hashTable10, long? hashTable11, long? hashTable12, long? hashTable13, long? hashTable14, long? hashTable15, long? hashTable16, long? hashTable17, long? hashTable18, long? hashTable19, long? hashTable20, long? hashTable21, long? hashTable22, long? hashTable23, long? hashTable24, string clusters, out int procResult)
        {
            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var sequenceNumberParam = new SqlParameter { ParameterName = "@SequenceNumber", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = sequenceNumber.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!sequenceNumber.HasValue)
                sequenceNumberParam.Value = System.DBNull.Value;

            var sequenceAtParam = new SqlParameter { ParameterName = "@SequenceAt", SqlDbType = SqlDbType.Float, Direction = ParameterDirection.Input, Value = sequenceAt.GetValueOrDefault(), Precision = 53, Scale = 0 };
            if (!sequenceAt.HasValue)
                sequenceAtParam.Value = System.DBNull.Value;

            var hashTable0Param = new SqlParameter { ParameterName = "@HashTable_0", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable0.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable0.HasValue)
                hashTable0Param.Value = System.DBNull.Value;

            var hashTable1Param = new SqlParameter { ParameterName = "@HashTable_1", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable1.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable1.HasValue)
                hashTable1Param.Value = System.DBNull.Value;

            var hashTable2Param = new SqlParameter { ParameterName = "@HashTable_2", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable2.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable2.HasValue)
                hashTable2Param.Value = System.DBNull.Value;

            var hashTable3Param = new SqlParameter { ParameterName = "@HashTable_3", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable3.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable3.HasValue)
                hashTable3Param.Value = System.DBNull.Value;

            var hashTable4Param = new SqlParameter { ParameterName = "@HashTable_4", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable4.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable4.HasValue)
                hashTable4Param.Value = System.DBNull.Value;

            var hashTable5Param = new SqlParameter { ParameterName = "@HashTable_5", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable5.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable5.HasValue)
                hashTable5Param.Value = System.DBNull.Value;

            var hashTable6Param = new SqlParameter { ParameterName = "@HashTable_6", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable6.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable6.HasValue)
                hashTable6Param.Value = System.DBNull.Value;

            var hashTable7Param = new SqlParameter { ParameterName = "@HashTable_7", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable7.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable7.HasValue)
                hashTable7Param.Value = System.DBNull.Value;

            var hashTable8Param = new SqlParameter { ParameterName = "@HashTable_8", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable8.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable8.HasValue)
                hashTable8Param.Value = System.DBNull.Value;

            var hashTable9Param = new SqlParameter { ParameterName = "@HashTable_9", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable9.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable9.HasValue)
                hashTable9Param.Value = System.DBNull.Value;

            var hashTable10Param = new SqlParameter { ParameterName = "@HashTable_10", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable10.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable10.HasValue)
                hashTable10Param.Value = System.DBNull.Value;

            var hashTable11Param = new SqlParameter { ParameterName = "@HashTable_11", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable11.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable11.HasValue)
                hashTable11Param.Value = System.DBNull.Value;

            var hashTable12Param = new SqlParameter { ParameterName = "@HashTable_12", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable12.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable12.HasValue)
                hashTable12Param.Value = System.DBNull.Value;

            var hashTable13Param = new SqlParameter { ParameterName = "@HashTable_13", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable13.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable13.HasValue)
                hashTable13Param.Value = System.DBNull.Value;

            var hashTable14Param = new SqlParameter { ParameterName = "@HashTable_14", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable14.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable14.HasValue)
                hashTable14Param.Value = System.DBNull.Value;

            var hashTable15Param = new SqlParameter { ParameterName = "@HashTable_15", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable15.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable15.HasValue)
                hashTable15Param.Value = System.DBNull.Value;

            var hashTable16Param = new SqlParameter { ParameterName = "@HashTable_16", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable16.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable16.HasValue)
                hashTable16Param.Value = System.DBNull.Value;

            var hashTable17Param = new SqlParameter { ParameterName = "@HashTable_17", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable17.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable17.HasValue)
                hashTable17Param.Value = System.DBNull.Value;

            var hashTable18Param = new SqlParameter { ParameterName = "@HashTable_18", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable18.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable18.HasValue)
                hashTable18Param.Value = System.DBNull.Value;

            var hashTable19Param = new SqlParameter { ParameterName = "@HashTable_19", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable19.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable19.HasValue)
                hashTable19Param.Value = System.DBNull.Value;

            var hashTable20Param = new SqlParameter { ParameterName = "@HashTable_20", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable20.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable20.HasValue)
                hashTable20Param.Value = System.DBNull.Value;

            var hashTable21Param = new SqlParameter { ParameterName = "@HashTable_21", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable21.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable21.HasValue)
                hashTable21Param.Value = System.DBNull.Value;

            var hashTable22Param = new SqlParameter { ParameterName = "@HashTable_22", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable22.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable22.HasValue)
                hashTable22Param.Value = System.DBNull.Value;

            var hashTable23Param = new SqlParameter { ParameterName = "@HashTable_23", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable23.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable23.HasValue)
                hashTable23Param.Value = System.DBNull.Value;

            var hashTable24Param = new SqlParameter { ParameterName = "@HashTable_24", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable24.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable24.HasValue)
                hashTable24Param.Value = System.DBNull.Value;

            var clustersParam = new SqlParameter { ParameterName = "@Clusters", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = clusters, Size = 255 };
            if (clustersParam.Value == null)
                clustersParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpInsertSubFingerprintReturnModel>("EXEC @procResult = [dbo].[sp_InsertSubFingerprint] @TrackId, @SequenceNumber, @SequenceAt, @HashTable_0, @HashTable_1, @HashTable_2, @HashTable_3, @HashTable_4, @HashTable_5, @HashTable_6, @HashTable_7, @HashTable_8, @HashTable_9, @HashTable_10, @HashTable_11, @HashTable_12, @HashTable_13, @HashTable_14, @HashTable_15, @HashTable_16, @HashTable_17, @HashTable_18, @HashTable_19, @HashTable_20, @HashTable_21, @HashTable_22, @HashTable_23, @HashTable_24, @Clusters", trackIdParam, sequenceNumberParam, sequenceAtParam, hashTable0Param, hashTable1Param, hashTable2Param, hashTable3Param, hashTable4Param, hashTable5Param, hashTable6Param, hashTable7Param, hashTable8Param, hashTable9Param, hashTable10Param, hashTable11Param, hashTable12Param, hashTable13Param, hashTable14Param, hashTable15Param, hashTable16Param, hashTable17Param, hashTable18Param, hashTable19Param, hashTable20Param, hashTable21Param, hashTable22Param, hashTable23Param, hashTable24Param, clustersParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpInsertSubFingerprintReturnModel>> SpInsertSubFingerprintAsync(int? trackId, int? sequenceNumber, double? sequenceAt, long? hashTable0, long? hashTable1, long? hashTable2, long? hashTable3, long? hashTable4, long? hashTable5, long? hashTable6, long? hashTable7, long? hashTable8, long? hashTable9, long? hashTable10, long? hashTable11, long? hashTable12, long? hashTable13, long? hashTable14, long? hashTable15, long? hashTable16, long? hashTable17, long? hashTable18, long? hashTable19, long? hashTable20, long? hashTable21, long? hashTable22, long? hashTable23, long? hashTable24, string clusters)
        {
            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var sequenceNumberParam = new SqlParameter { ParameterName = "@SequenceNumber", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = sequenceNumber.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!sequenceNumber.HasValue)
                sequenceNumberParam.Value = System.DBNull.Value;

            var sequenceAtParam = new SqlParameter { ParameterName = "@SequenceAt", SqlDbType = SqlDbType.Float, Direction = ParameterDirection.Input, Value = sequenceAt.GetValueOrDefault(), Precision = 53, Scale = 0 };
            if (!sequenceAt.HasValue)
                sequenceAtParam.Value = System.DBNull.Value;

            var hashTable0Param = new SqlParameter { ParameterName = "@HashTable_0", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable0.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable0.HasValue)
                hashTable0Param.Value = System.DBNull.Value;

            var hashTable1Param = new SqlParameter { ParameterName = "@HashTable_1", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable1.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable1.HasValue)
                hashTable1Param.Value = System.DBNull.Value;

            var hashTable2Param = new SqlParameter { ParameterName = "@HashTable_2", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable2.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable2.HasValue)
                hashTable2Param.Value = System.DBNull.Value;

            var hashTable3Param = new SqlParameter { ParameterName = "@HashTable_3", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable3.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable3.HasValue)
                hashTable3Param.Value = System.DBNull.Value;

            var hashTable4Param = new SqlParameter { ParameterName = "@HashTable_4", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable4.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable4.HasValue)
                hashTable4Param.Value = System.DBNull.Value;

            var hashTable5Param = new SqlParameter { ParameterName = "@HashTable_5", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable5.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable5.HasValue)
                hashTable5Param.Value = System.DBNull.Value;

            var hashTable6Param = new SqlParameter { ParameterName = "@HashTable_6", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable6.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable6.HasValue)
                hashTable6Param.Value = System.DBNull.Value;

            var hashTable7Param = new SqlParameter { ParameterName = "@HashTable_7", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable7.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable7.HasValue)
                hashTable7Param.Value = System.DBNull.Value;

            var hashTable8Param = new SqlParameter { ParameterName = "@HashTable_8", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable8.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable8.HasValue)
                hashTable8Param.Value = System.DBNull.Value;

            var hashTable9Param = new SqlParameter { ParameterName = "@HashTable_9", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable9.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable9.HasValue)
                hashTable9Param.Value = System.DBNull.Value;

            var hashTable10Param = new SqlParameter { ParameterName = "@HashTable_10", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable10.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable10.HasValue)
                hashTable10Param.Value = System.DBNull.Value;

            var hashTable11Param = new SqlParameter { ParameterName = "@HashTable_11", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable11.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable11.HasValue)
                hashTable11Param.Value = System.DBNull.Value;

            var hashTable12Param = new SqlParameter { ParameterName = "@HashTable_12", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable12.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable12.HasValue)
                hashTable12Param.Value = System.DBNull.Value;

            var hashTable13Param = new SqlParameter { ParameterName = "@HashTable_13", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable13.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable13.HasValue)
                hashTable13Param.Value = System.DBNull.Value;

            var hashTable14Param = new SqlParameter { ParameterName = "@HashTable_14", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable14.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable14.HasValue)
                hashTable14Param.Value = System.DBNull.Value;

            var hashTable15Param = new SqlParameter { ParameterName = "@HashTable_15", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable15.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable15.HasValue)
                hashTable15Param.Value = System.DBNull.Value;

            var hashTable16Param = new SqlParameter { ParameterName = "@HashTable_16", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable16.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable16.HasValue)
                hashTable16Param.Value = System.DBNull.Value;

            var hashTable17Param = new SqlParameter { ParameterName = "@HashTable_17", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable17.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable17.HasValue)
                hashTable17Param.Value = System.DBNull.Value;

            var hashTable18Param = new SqlParameter { ParameterName = "@HashTable_18", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable18.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable18.HasValue)
                hashTable18Param.Value = System.DBNull.Value;

            var hashTable19Param = new SqlParameter { ParameterName = "@HashTable_19", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable19.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable19.HasValue)
                hashTable19Param.Value = System.DBNull.Value;

            var hashTable20Param = new SqlParameter { ParameterName = "@HashTable_20", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable20.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable20.HasValue)
                hashTable20Param.Value = System.DBNull.Value;

            var hashTable21Param = new SqlParameter { ParameterName = "@HashTable_21", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable21.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable21.HasValue)
                hashTable21Param.Value = System.DBNull.Value;

            var hashTable22Param = new SqlParameter { ParameterName = "@HashTable_22", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable22.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable22.HasValue)
                hashTable22Param.Value = System.DBNull.Value;

            var hashTable23Param = new SqlParameter { ParameterName = "@HashTable_23", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable23.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable23.HasValue)
                hashTable23Param.Value = System.DBNull.Value;

            var hashTable24Param = new SqlParameter { ParameterName = "@HashTable_24", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashTable24.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashTable24.HasValue)
                hashTable24Param.Value = System.DBNull.Value;

            var clustersParam = new SqlParameter { ParameterName = "@Clusters", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = clusters, Size = 255 };
            if (clustersParam.Value == null)
                clustersParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpInsertSubFingerprintReturnModel>("EXEC [dbo].[sp_InsertSubFingerprint] @TrackId, @SequenceNumber, @SequenceAt, @HashTable_0, @HashTable_1, @HashTable_2, @HashTable_3, @HashTable_4, @HashTable_5, @HashTable_6, @HashTable_7, @HashTable_8, @HashTable_9, @HashTable_10, @HashTable_11, @HashTable_12, @HashTable_13, @HashTable_14, @HashTable_15, @HashTable_16, @HashTable_17, @HashTable_18, @HashTable_19, @HashTable_20, @HashTable_21, @HashTable_22, @HashTable_23, @HashTable_24, @Clusters", trackIdParam, sequenceNumberParam, sequenceAtParam, hashTable0Param, hashTable1Param, hashTable2Param, hashTable3Param, hashTable4Param, hashTable5Param, hashTable6Param, hashTable7Param, hashTable8Param, hashTable9Param, hashTable10Param, hashTable11Param, hashTable12Param, hashTable13Param, hashTable14Param, hashTable15Param, hashTable16Param, hashTable17Param, hashTable18Param, hashTable19Param, hashTable20Param, hashTable21Param, hashTable22Param, hashTable23Param, hashTable24Param, clustersParam).ToListAsync();

            return procResultData;
        }

        public List<SpInsertTrackReturnModel> SpInsertTrack(string isrc, string artist, string title, string album, int? releaseYear, double? length)
        {
            int procResult;
            return SpInsertTrack(isrc, artist, title, album, releaseYear, length, out procResult);
        }

        public List<SpInsertTrackReturnModel> SpInsertTrack(string isrc, string artist, string title, string album, int? releaseYear, double? length, out int procResult)
        {
            var isrcParam = new SqlParameter { ParameterName = "@ISRC", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = isrc, Size = 50 };
            if (isrcParam.Value == null)
                isrcParam.Value = System.DBNull.Value;

            var artistParam = new SqlParameter { ParameterName = "@Artist", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = artist, Size = 255 };
            if (artistParam.Value == null)
                artistParam.Value = System.DBNull.Value;

            var titleParam = new SqlParameter { ParameterName = "@Title", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = title, Size = 255 };
            if (titleParam.Value == null)
                titleParam.Value = System.DBNull.Value;

            var albumParam = new SqlParameter { ParameterName = "@Album", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = album, Size = 255 };
            if (albumParam.Value == null)
                albumParam.Value = System.DBNull.Value;

            var releaseYearParam = new SqlParameter { ParameterName = "@ReleaseYear", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = releaseYear.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!releaseYear.HasValue)
                releaseYearParam.Value = System.DBNull.Value;

            var lengthParam = new SqlParameter { ParameterName = "@Length", SqlDbType = SqlDbType.Float, Direction = ParameterDirection.Input, Value = length.GetValueOrDefault(), Precision = 53, Scale = 0 };
            if (!length.HasValue)
                lengthParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpInsertTrackReturnModel>("EXEC @procResult = [dbo].[sp_InsertTrack] @ISRC, @Artist, @Title, @Album, @ReleaseYear, @Length", isrcParam, artistParam, titleParam, albumParam, releaseYearParam, lengthParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpInsertTrackReturnModel>> SpInsertTrackAsync(string isrc, string artist, string title, string album, int? releaseYear, double? length)
        {
            var isrcParam = new SqlParameter { ParameterName = "@ISRC", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = isrc, Size = 50 };
            if (isrcParam.Value == null)
                isrcParam.Value = System.DBNull.Value;

            var artistParam = new SqlParameter { ParameterName = "@Artist", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = artist, Size = 255 };
            if (artistParam.Value == null)
                artistParam.Value = System.DBNull.Value;

            var titleParam = new SqlParameter { ParameterName = "@Title", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = title, Size = 255 };
            if (titleParam.Value == null)
                titleParam.Value = System.DBNull.Value;

            var albumParam = new SqlParameter { ParameterName = "@Album", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = album, Size = 255 };
            if (albumParam.Value == null)
                albumParam.Value = System.DBNull.Value;

            var releaseYearParam = new SqlParameter { ParameterName = "@ReleaseYear", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = releaseYear.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!releaseYear.HasValue)
                releaseYearParam.Value = System.DBNull.Value;

            var lengthParam = new SqlParameter { ParameterName = "@Length", SqlDbType = SqlDbType.Float, Direction = ParameterDirection.Input, Value = length.GetValueOrDefault(), Precision = 53, Scale = 0 };
            if (!length.HasValue)
                lengthParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpInsertTrackReturnModel>("EXEC [dbo].[sp_InsertTrack] @ISRC, @Artist, @Title, @Album, @ReleaseYear, @Length", isrcParam, artistParam, titleParam, albumParam, releaseYearParam, lengthParam).ToListAsync();

            return procResultData;
        }

        public List<SpMisGetActiveStationsReturnModel> SpMisGetActiveStations()
        {
            int procResult;
            return SpMisGetActiveStations(out procResult);
        }

        public List<SpMisGetActiveStationsReturnModel> SpMisGetActiveStations(out int procResult)
        {
            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpMisGetActiveStationsReturnModel>("EXEC @procResult = [dbo].[sp_mis_GetActiveStations] ", procResultParam).ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpMisGetActiveStationsReturnModel>> SpMisGetActiveStationsAsync()
        {
            var procResultData = await Database.SqlQuery<SpMisGetActiveStationsReturnModel>("EXEC [dbo].[sp_mis_GetActiveStations] ").ToListAsync();

            return procResultData;
        }

        public List<SpGetUnprocessedStreamsReturnModel> SpGetUnprocessedStreams()
        {
            int procResult;
            return SpGetUnprocessedStreams(out procResult);
        }

        public List<SpGetUnprocessedStreamsReturnModel> SpGetUnprocessedStreams(out int procResult)
        {
            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpGetUnprocessedStreamsReturnModel>("EXEC @procResult = [dbo].[sp_GetUnprocessedStreams] ", procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpGetUnprocessedStreamsReturnModel>> SpGetUnprocessedStreamsAsync()
        {
            var procResultData = await Database.SqlQuery<SpGetUnprocessedStreamsReturnModel>("EXEC [dbo].[sp_GetUnprocessedStreams] ").ToListAsync();

            return procResultData;
        }

        public List<SpReadFingerprintByTrackIdReturnModel> SpReadFingerprintByTrackId(int? trackId)
        {
            int procResult;
            return SpReadFingerprintByTrackId(trackId, out procResult);
        }

        public List<SpReadFingerprintByTrackIdReturnModel> SpReadFingerprintByTrackId(int? trackId, out int procResult)
        {
            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadFingerprintByTrackIdReturnModel>("EXEC @procResult = [dbo].[sp_ReadFingerprintByTrackId] @TrackId", trackIdParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadFingerprintByTrackIdReturnModel>> SpReadFingerprintByTrackIdAsync(int? trackId)
        {
            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadFingerprintByTrackIdReturnModel>("EXEC [dbo].[sp_ReadFingerprintByTrackId] @TrackId", trackIdParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel> SpReadFingerprintsByHashBinHashTableAndThreshold(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold)
        {
            int procResult;
            return SpReadFingerprintsByHashBinHashTableAndThreshold(hashBin0, hashBin1, hashBin2, hashBin3, hashBin4, hashBin5, hashBin6, hashBin7, hashBin8, hashBin9, hashBin10, hashBin11, hashBin12, hashBin13, hashBin14, hashBin15, hashBin16, hashBin17, hashBin18, hashBin19, hashBin20, hashBin21, hashBin22, hashBin23, hashBin24, threshold, out procResult);
        }

        public List<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel> SpReadFingerprintsByHashBinHashTableAndThreshold(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, out int procResult)
        {
            var hashBin0Param = new SqlParameter { ParameterName = "@HashBin_0", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin0.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin0.HasValue)
                hashBin0Param.Value = System.DBNull.Value;

            var hashBin1Param = new SqlParameter { ParameterName = "@HashBin_1", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin1.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin1.HasValue)
                hashBin1Param.Value = System.DBNull.Value;

            var hashBin2Param = new SqlParameter { ParameterName = "@HashBin_2", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin2.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin2.HasValue)
                hashBin2Param.Value = System.DBNull.Value;

            var hashBin3Param = new SqlParameter { ParameterName = "@HashBin_3", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin3.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin3.HasValue)
                hashBin3Param.Value = System.DBNull.Value;

            var hashBin4Param = new SqlParameter { ParameterName = "@HashBin_4", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin4.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin4.HasValue)
                hashBin4Param.Value = System.DBNull.Value;

            var hashBin5Param = new SqlParameter { ParameterName = "@HashBin_5", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin5.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin5.HasValue)
                hashBin5Param.Value = System.DBNull.Value;

            var hashBin6Param = new SqlParameter { ParameterName = "@HashBin_6", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin6.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin6.HasValue)
                hashBin6Param.Value = System.DBNull.Value;

            var hashBin7Param = new SqlParameter { ParameterName = "@HashBin_7", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin7.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin7.HasValue)
                hashBin7Param.Value = System.DBNull.Value;

            var hashBin8Param = new SqlParameter { ParameterName = "@HashBin_8", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin8.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin8.HasValue)
                hashBin8Param.Value = System.DBNull.Value;

            var hashBin9Param = new SqlParameter { ParameterName = "@HashBin_9", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin9.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin9.HasValue)
                hashBin9Param.Value = System.DBNull.Value;

            var hashBin10Param = new SqlParameter { ParameterName = "@HashBin_10", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin10.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin10.HasValue)
                hashBin10Param.Value = System.DBNull.Value;

            var hashBin11Param = new SqlParameter { ParameterName = "@HashBin_11", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin11.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin11.HasValue)
                hashBin11Param.Value = System.DBNull.Value;

            var hashBin12Param = new SqlParameter { ParameterName = "@HashBin_12", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin12.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin12.HasValue)
                hashBin12Param.Value = System.DBNull.Value;

            var hashBin13Param = new SqlParameter { ParameterName = "@HashBin_13", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin13.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin13.HasValue)
                hashBin13Param.Value = System.DBNull.Value;

            var hashBin14Param = new SqlParameter { ParameterName = "@HashBin_14", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin14.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin14.HasValue)
                hashBin14Param.Value = System.DBNull.Value;

            var hashBin15Param = new SqlParameter { ParameterName = "@HashBin_15", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin15.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin15.HasValue)
                hashBin15Param.Value = System.DBNull.Value;

            var hashBin16Param = new SqlParameter { ParameterName = "@HashBin_16", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin16.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin16.HasValue)
                hashBin16Param.Value = System.DBNull.Value;

            var hashBin17Param = new SqlParameter { ParameterName = "@HashBin_17", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin17.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin17.HasValue)
                hashBin17Param.Value = System.DBNull.Value;

            var hashBin18Param = new SqlParameter { ParameterName = "@HashBin_18", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin18.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin18.HasValue)
                hashBin18Param.Value = System.DBNull.Value;

            var hashBin19Param = new SqlParameter { ParameterName = "@HashBin_19", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin19.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin19.HasValue)
                hashBin19Param.Value = System.DBNull.Value;

            var hashBin20Param = new SqlParameter { ParameterName = "@HashBin_20", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin20.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin20.HasValue)
                hashBin20Param.Value = System.DBNull.Value;

            var hashBin21Param = new SqlParameter { ParameterName = "@HashBin_21", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin21.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin21.HasValue)
                hashBin21Param.Value = System.DBNull.Value;

            var hashBin22Param = new SqlParameter { ParameterName = "@HashBin_22", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin22.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin22.HasValue)
                hashBin22Param.Value = System.DBNull.Value;

            var hashBin23Param = new SqlParameter { ParameterName = "@HashBin_23", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin23.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin23.HasValue)
                hashBin23Param.Value = System.DBNull.Value;

            var hashBin24Param = new SqlParameter { ParameterName = "@HashBin_24", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin24.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin24.HasValue)
                hashBin24Param.Value = System.DBNull.Value;

            var thresholdParam = new SqlParameter { ParameterName = "@Threshold", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = threshold.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!threshold.HasValue)
                thresholdParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel>("EXEC @procResult = [dbo].[sp_ReadFingerprintsByHashBinHashTableAndThreshold] @HashBin_0, @HashBin_1, @HashBin_2, @HashBin_3, @HashBin_4, @HashBin_5, @HashBin_6, @HashBin_7, @HashBin_8, @HashBin_9, @HashBin_10, @HashBin_11, @HashBin_12, @HashBin_13, @HashBin_14, @HashBin_15, @HashBin_16, @HashBin_17, @HashBin_18, @HashBin_19, @HashBin_20, @HashBin_21, @HashBin_22, @HashBin_23, @HashBin_24, @Threshold", hashBin0Param, hashBin1Param, hashBin2Param, hashBin3Param, hashBin4Param, hashBin5Param, hashBin6Param, hashBin7Param, hashBin8Param, hashBin9Param, hashBin10Param, hashBin11Param, hashBin12Param, hashBin13Param, hashBin14Param, hashBin15Param, hashBin16Param, hashBin17Param, hashBin18Param, hashBin19Param, hashBin20Param, hashBin21Param, hashBin22Param, hashBin23Param, hashBin24Param, thresholdParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel>> SpReadFingerprintsByHashBinHashTableAndThresholdAsync(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold)
        {
            var hashBin0Param = new SqlParameter { ParameterName = "@HashBin_0", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin0.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin0.HasValue)
                hashBin0Param.Value = System.DBNull.Value;

            var hashBin1Param = new SqlParameter { ParameterName = "@HashBin_1", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin1.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin1.HasValue)
                hashBin1Param.Value = System.DBNull.Value;

            var hashBin2Param = new SqlParameter { ParameterName = "@HashBin_2", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin2.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin2.HasValue)
                hashBin2Param.Value = System.DBNull.Value;

            var hashBin3Param = new SqlParameter { ParameterName = "@HashBin_3", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin3.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin3.HasValue)
                hashBin3Param.Value = System.DBNull.Value;

            var hashBin4Param = new SqlParameter { ParameterName = "@HashBin_4", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin4.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin4.HasValue)
                hashBin4Param.Value = System.DBNull.Value;

            var hashBin5Param = new SqlParameter { ParameterName = "@HashBin_5", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin5.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin5.HasValue)
                hashBin5Param.Value = System.DBNull.Value;

            var hashBin6Param = new SqlParameter { ParameterName = "@HashBin_6", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin6.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin6.HasValue)
                hashBin6Param.Value = System.DBNull.Value;

            var hashBin7Param = new SqlParameter { ParameterName = "@HashBin_7", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin7.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin7.HasValue)
                hashBin7Param.Value = System.DBNull.Value;

            var hashBin8Param = new SqlParameter { ParameterName = "@HashBin_8", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin8.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin8.HasValue)
                hashBin8Param.Value = System.DBNull.Value;

            var hashBin9Param = new SqlParameter { ParameterName = "@HashBin_9", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin9.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin9.HasValue)
                hashBin9Param.Value = System.DBNull.Value;

            var hashBin10Param = new SqlParameter { ParameterName = "@HashBin_10", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin10.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin10.HasValue)
                hashBin10Param.Value = System.DBNull.Value;

            var hashBin11Param = new SqlParameter { ParameterName = "@HashBin_11", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin11.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin11.HasValue)
                hashBin11Param.Value = System.DBNull.Value;

            var hashBin12Param = new SqlParameter { ParameterName = "@HashBin_12", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin12.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin12.HasValue)
                hashBin12Param.Value = System.DBNull.Value;

            var hashBin13Param = new SqlParameter { ParameterName = "@HashBin_13", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin13.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin13.HasValue)
                hashBin13Param.Value = System.DBNull.Value;

            var hashBin14Param = new SqlParameter { ParameterName = "@HashBin_14", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin14.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin14.HasValue)
                hashBin14Param.Value = System.DBNull.Value;

            var hashBin15Param = new SqlParameter { ParameterName = "@HashBin_15", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin15.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin15.HasValue)
                hashBin15Param.Value = System.DBNull.Value;

            var hashBin16Param = new SqlParameter { ParameterName = "@HashBin_16", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin16.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin16.HasValue)
                hashBin16Param.Value = System.DBNull.Value;

            var hashBin17Param = new SqlParameter { ParameterName = "@HashBin_17", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin17.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin17.HasValue)
                hashBin17Param.Value = System.DBNull.Value;

            var hashBin18Param = new SqlParameter { ParameterName = "@HashBin_18", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin18.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin18.HasValue)
                hashBin18Param.Value = System.DBNull.Value;

            var hashBin19Param = new SqlParameter { ParameterName = "@HashBin_19", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin19.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin19.HasValue)
                hashBin19Param.Value = System.DBNull.Value;

            var hashBin20Param = new SqlParameter { ParameterName = "@HashBin_20", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin20.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin20.HasValue)
                hashBin20Param.Value = System.DBNull.Value;

            var hashBin21Param = new SqlParameter { ParameterName = "@HashBin_21", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin21.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin21.HasValue)
                hashBin21Param.Value = System.DBNull.Value;

            var hashBin22Param = new SqlParameter { ParameterName = "@HashBin_22", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin22.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin22.HasValue)
                hashBin22Param.Value = System.DBNull.Value;

            var hashBin23Param = new SqlParameter { ParameterName = "@HashBin_23", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin23.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin23.HasValue)
                hashBin23Param.Value = System.DBNull.Value;

            var hashBin24Param = new SqlParameter { ParameterName = "@HashBin_24", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin24.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin24.HasValue)
                hashBin24Param.Value = System.DBNull.Value;

            var thresholdParam = new SqlParameter { ParameterName = "@Threshold", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = threshold.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!threshold.HasValue)
                thresholdParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel>("EXEC [dbo].[sp_ReadFingerprintsByHashBinHashTableAndThreshold] @HashBin_0, @HashBin_1, @HashBin_2, @HashBin_3, @HashBin_4, @HashBin_5, @HashBin_6, @HashBin_7, @HashBin_8, @HashBin_9, @HashBin_10, @HashBin_11, @HashBin_12, @HashBin_13, @HashBin_14, @HashBin_15, @HashBin_16, @HashBin_17, @HashBin_18, @HashBin_19, @HashBin_20, @HashBin_21, @HashBin_22, @HashBin_23, @HashBin_24, @Threshold", hashBin0Param, hashBin1Param, hashBin2Param, hashBin3Param, hashBin4Param, hashBin5Param, hashBin6Param, hashBin7Param, hashBin8Param, hashBin9Param, hashBin10Param, hashBin11Param, hashBin12Param, hashBin13Param, hashBin14Param, hashBin15Param, hashBin16Param, hashBin17Param, hashBin18Param, hashBin19Param, hashBin20Param, hashBin21Param, hashBin22Param, hashBin23Param, hashBin24Param, thresholdParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel> SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, string clusters)
        {
            int procResult;
            return SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters(hashBin0, hashBin1, hashBin2, hashBin3, hashBin4, hashBin5, hashBin6, hashBin7, hashBin8, hashBin9, hashBin10, hashBin11, hashBin12, hashBin13, hashBin14, hashBin15, hashBin16, hashBin17, hashBin18, hashBin19, hashBin20, hashBin21, hashBin22, hashBin23, hashBin24, threshold, clusters, out procResult);
        }

        public List<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel> SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, string clusters, out int procResult)
        {
            var hashBin0Param = new SqlParameter { ParameterName = "@HashBin_0", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin0.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin0.HasValue)
                hashBin0Param.Value = System.DBNull.Value;

            var hashBin1Param = new SqlParameter { ParameterName = "@HashBin_1", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin1.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin1.HasValue)
                hashBin1Param.Value = System.DBNull.Value;

            var hashBin2Param = new SqlParameter { ParameterName = "@HashBin_2", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin2.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin2.HasValue)
                hashBin2Param.Value = System.DBNull.Value;

            var hashBin3Param = new SqlParameter { ParameterName = "@HashBin_3", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin3.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin3.HasValue)
                hashBin3Param.Value = System.DBNull.Value;

            var hashBin4Param = new SqlParameter { ParameterName = "@HashBin_4", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin4.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin4.HasValue)
                hashBin4Param.Value = System.DBNull.Value;

            var hashBin5Param = new SqlParameter { ParameterName = "@HashBin_5", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin5.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin5.HasValue)
                hashBin5Param.Value = System.DBNull.Value;

            var hashBin6Param = new SqlParameter { ParameterName = "@HashBin_6", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin6.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin6.HasValue)
                hashBin6Param.Value = System.DBNull.Value;

            var hashBin7Param = new SqlParameter { ParameterName = "@HashBin_7", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin7.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin7.HasValue)
                hashBin7Param.Value = System.DBNull.Value;

            var hashBin8Param = new SqlParameter { ParameterName = "@HashBin_8", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin8.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin8.HasValue)
                hashBin8Param.Value = System.DBNull.Value;

            var hashBin9Param = new SqlParameter { ParameterName = "@HashBin_9", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin9.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin9.HasValue)
                hashBin9Param.Value = System.DBNull.Value;

            var hashBin10Param = new SqlParameter { ParameterName = "@HashBin_10", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin10.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin10.HasValue)
                hashBin10Param.Value = System.DBNull.Value;

            var hashBin11Param = new SqlParameter { ParameterName = "@HashBin_11", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin11.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin11.HasValue)
                hashBin11Param.Value = System.DBNull.Value;

            var hashBin12Param = new SqlParameter { ParameterName = "@HashBin_12", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin12.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin12.HasValue)
                hashBin12Param.Value = System.DBNull.Value;

            var hashBin13Param = new SqlParameter { ParameterName = "@HashBin_13", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin13.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin13.HasValue)
                hashBin13Param.Value = System.DBNull.Value;

            var hashBin14Param = new SqlParameter { ParameterName = "@HashBin_14", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin14.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin14.HasValue)
                hashBin14Param.Value = System.DBNull.Value;

            var hashBin15Param = new SqlParameter { ParameterName = "@HashBin_15", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin15.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin15.HasValue)
                hashBin15Param.Value = System.DBNull.Value;

            var hashBin16Param = new SqlParameter { ParameterName = "@HashBin_16", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin16.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin16.HasValue)
                hashBin16Param.Value = System.DBNull.Value;

            var hashBin17Param = new SqlParameter { ParameterName = "@HashBin_17", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin17.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin17.HasValue)
                hashBin17Param.Value = System.DBNull.Value;

            var hashBin18Param = new SqlParameter { ParameterName = "@HashBin_18", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin18.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin18.HasValue)
                hashBin18Param.Value = System.DBNull.Value;

            var hashBin19Param = new SqlParameter { ParameterName = "@HashBin_19", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin19.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin19.HasValue)
                hashBin19Param.Value = System.DBNull.Value;

            var hashBin20Param = new SqlParameter { ParameterName = "@HashBin_20", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin20.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin20.HasValue)
                hashBin20Param.Value = System.DBNull.Value;

            var hashBin21Param = new SqlParameter { ParameterName = "@HashBin_21", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin21.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin21.HasValue)
                hashBin21Param.Value = System.DBNull.Value;

            var hashBin22Param = new SqlParameter { ParameterName = "@HashBin_22", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin22.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin22.HasValue)
                hashBin22Param.Value = System.DBNull.Value;

            var hashBin23Param = new SqlParameter { ParameterName = "@HashBin_23", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin23.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin23.HasValue)
                hashBin23Param.Value = System.DBNull.Value;

            var hashBin24Param = new SqlParameter { ParameterName = "@HashBin_24", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin24.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin24.HasValue)
                hashBin24Param.Value = System.DBNull.Value;

            var thresholdParam = new SqlParameter { ParameterName = "@Threshold", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = threshold.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!threshold.HasValue)
                thresholdParam.Value = System.DBNull.Value;

            var clustersParam = new SqlParameter { ParameterName = "@Clusters", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = clusters, Size = 255 };
            if (clustersParam.Value == null)
                clustersParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel>("EXEC @procResult = [dbo].[sp_ReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters] @HashBin_0, @HashBin_1, @HashBin_2, @HashBin_3, @HashBin_4, @HashBin_5, @HashBin_6, @HashBin_7, @HashBin_8, @HashBin_9, @HashBin_10, @HashBin_11, @HashBin_12, @HashBin_13, @HashBin_14, @HashBin_15, @HashBin_16, @HashBin_17, @HashBin_18, @HashBin_19, @HashBin_20, @HashBin_21, @HashBin_22, @HashBin_23, @HashBin_24, @Threshold, @Clusters", hashBin0Param, hashBin1Param, hashBin2Param, hashBin3Param, hashBin4Param, hashBin5Param, hashBin6Param, hashBin7Param, hashBin8Param, hashBin9Param, hashBin10Param, hashBin11Param, hashBin12Param, hashBin13Param, hashBin14Param, hashBin15Param, hashBin16Param, hashBin17Param, hashBin18Param, hashBin19Param, hashBin20Param, hashBin21Param, hashBin22Param, hashBin23Param, hashBin24Param, thresholdParam, clustersParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel>> SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersAsync(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, string clusters)
        {
            var hashBin0Param = new SqlParameter { ParameterName = "@HashBin_0", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin0.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin0.HasValue)
                hashBin0Param.Value = System.DBNull.Value;

            var hashBin1Param = new SqlParameter { ParameterName = "@HashBin_1", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin1.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin1.HasValue)
                hashBin1Param.Value = System.DBNull.Value;

            var hashBin2Param = new SqlParameter { ParameterName = "@HashBin_2", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin2.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin2.HasValue)
                hashBin2Param.Value = System.DBNull.Value;

            var hashBin3Param = new SqlParameter { ParameterName = "@HashBin_3", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin3.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin3.HasValue)
                hashBin3Param.Value = System.DBNull.Value;

            var hashBin4Param = new SqlParameter { ParameterName = "@HashBin_4", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin4.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin4.HasValue)
                hashBin4Param.Value = System.DBNull.Value;

            var hashBin5Param = new SqlParameter { ParameterName = "@HashBin_5", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin5.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin5.HasValue)
                hashBin5Param.Value = System.DBNull.Value;

            var hashBin6Param = new SqlParameter { ParameterName = "@HashBin_6", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin6.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin6.HasValue)
                hashBin6Param.Value = System.DBNull.Value;

            var hashBin7Param = new SqlParameter { ParameterName = "@HashBin_7", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin7.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin7.HasValue)
                hashBin7Param.Value = System.DBNull.Value;

            var hashBin8Param = new SqlParameter { ParameterName = "@HashBin_8", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin8.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin8.HasValue)
                hashBin8Param.Value = System.DBNull.Value;

            var hashBin9Param = new SqlParameter { ParameterName = "@HashBin_9", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin9.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin9.HasValue)
                hashBin9Param.Value = System.DBNull.Value;

            var hashBin10Param = new SqlParameter { ParameterName = "@HashBin_10", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin10.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin10.HasValue)
                hashBin10Param.Value = System.DBNull.Value;

            var hashBin11Param = new SqlParameter { ParameterName = "@HashBin_11", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin11.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin11.HasValue)
                hashBin11Param.Value = System.DBNull.Value;

            var hashBin12Param = new SqlParameter { ParameterName = "@HashBin_12", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin12.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin12.HasValue)
                hashBin12Param.Value = System.DBNull.Value;

            var hashBin13Param = new SqlParameter { ParameterName = "@HashBin_13", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin13.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin13.HasValue)
                hashBin13Param.Value = System.DBNull.Value;

            var hashBin14Param = new SqlParameter { ParameterName = "@HashBin_14", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin14.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin14.HasValue)
                hashBin14Param.Value = System.DBNull.Value;

            var hashBin15Param = new SqlParameter { ParameterName = "@HashBin_15", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin15.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin15.HasValue)
                hashBin15Param.Value = System.DBNull.Value;

            var hashBin16Param = new SqlParameter { ParameterName = "@HashBin_16", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin16.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin16.HasValue)
                hashBin16Param.Value = System.DBNull.Value;

            var hashBin17Param = new SqlParameter { ParameterName = "@HashBin_17", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin17.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin17.HasValue)
                hashBin17Param.Value = System.DBNull.Value;

            var hashBin18Param = new SqlParameter { ParameterName = "@HashBin_18", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin18.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin18.HasValue)
                hashBin18Param.Value = System.DBNull.Value;

            var hashBin19Param = new SqlParameter { ParameterName = "@HashBin_19", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin19.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin19.HasValue)
                hashBin19Param.Value = System.DBNull.Value;

            var hashBin20Param = new SqlParameter { ParameterName = "@HashBin_20", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin20.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin20.HasValue)
                hashBin20Param.Value = System.DBNull.Value;

            var hashBin21Param = new SqlParameter { ParameterName = "@HashBin_21", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin21.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin21.HasValue)
                hashBin21Param.Value = System.DBNull.Value;

            var hashBin22Param = new SqlParameter { ParameterName = "@HashBin_22", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin22.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin22.HasValue)
                hashBin22Param.Value = System.DBNull.Value;

            var hashBin23Param = new SqlParameter { ParameterName = "@HashBin_23", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin23.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin23.HasValue)
                hashBin23Param.Value = System.DBNull.Value;

            var hashBin24Param = new SqlParameter { ParameterName = "@HashBin_24", SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input, Value = hashBin24.GetValueOrDefault(), Precision = 19, Scale = 0 };
            if (!hashBin24.HasValue)
                hashBin24Param.Value = System.DBNull.Value;

            var thresholdParam = new SqlParameter { ParameterName = "@Threshold", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = threshold.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!threshold.HasValue)
                thresholdParam.Value = System.DBNull.Value;

            var clustersParam = new SqlParameter { ParameterName = "@Clusters", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = clusters, Size = 255 };
            if (clustersParam.Value == null)
                clustersParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel>("EXEC [dbo].[sp_ReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters] @HashBin_0, @HashBin_1, @HashBin_2, @HashBin_3, @HashBin_4, @HashBin_5, @HashBin_6, @HashBin_7, @HashBin_8, @HashBin_9, @HashBin_10, @HashBin_11, @HashBin_12, @HashBin_13, @HashBin_14, @HashBin_15, @HashBin_16, @HashBin_17, @HashBin_18, @HashBin_19, @HashBin_20, @HashBin_21, @HashBin_22, @HashBin_23, @HashBin_24, @Threshold, @Clusters", hashBin0Param, hashBin1Param, hashBin2Param, hashBin3Param, hashBin4Param, hashBin5Param, hashBin6Param, hashBin7Param, hashBin8Param, hashBin9Param, hashBin10Param, hashBin11Param, hashBin12Param, hashBin13Param, hashBin14Param, hashBin15Param, hashBin16Param, hashBin17Param, hashBin18Param, hashBin19Param, hashBin20Param, hashBin21Param, hashBin22Param, hashBin23Param, hashBin24Param, thresholdParam, clustersParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadSubFingerprintsByTrackIdReturnModel> SpReadSubFingerprintsByTrackId(int? trackId)
        {
            int procResult;
            return SpReadSubFingerprintsByTrackId(trackId, out procResult);
        }

        public List<SpReadSubFingerprintsByTrackIdReturnModel> SpReadSubFingerprintsByTrackId(int? trackId, out int procResult)
        {
            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadSubFingerprintsByTrackIdReturnModel>("EXEC @procResult = [dbo].[sp_ReadSubFingerprintsByTrackId] @TrackId", trackIdParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadSubFingerprintsByTrackIdReturnModel>> SpReadSubFingerprintsByTrackIdAsync(int? trackId)
        {
            var trackIdParam = new SqlParameter { ParameterName = "@TrackId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = trackId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!trackId.HasValue)
                trackIdParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadSubFingerprintsByTrackIdReturnModel>("EXEC [dbo].[sp_ReadSubFingerprintsByTrackId] @TrackId", trackIdParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadTrackByArtistAndSongNameReturnModel> SpReadTrackByArtistAndSongName(string artist, string title)
        {
            int procResult;
            return SpReadTrackByArtistAndSongName(artist, title, out procResult);
        }

        public List<SpReadTrackByArtistAndSongNameReturnModel> SpReadTrackByArtistAndSongName(string artist, string title, out int procResult)
        {
            var artistParam = new SqlParameter { ParameterName = "@Artist", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = artist, Size = 255 };
            if (artistParam.Value == null)
                artistParam.Value = System.DBNull.Value;

            var titleParam = new SqlParameter { ParameterName = "@Title", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = title, Size = 255 };
            if (titleParam.Value == null)
                titleParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadTrackByArtistAndSongNameReturnModel>("EXEC @procResult = [dbo].[sp_ReadTrackByArtistAndSongName] @Artist, @Title", artistParam, titleParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadTrackByArtistAndSongNameReturnModel>> SpReadTrackByArtistAndSongNameAsync(string artist, string title)
        {
            var artistParam = new SqlParameter { ParameterName = "@Artist", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = artist, Size = 255 };
            if (artistParam.Value == null)
                artistParam.Value = System.DBNull.Value;

            var titleParam = new SqlParameter { ParameterName = "@Title", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = title, Size = 255 };
            if (titleParam.Value == null)
                titleParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadTrackByArtistAndSongNameReturnModel>("EXEC [dbo].[sp_ReadTrackByArtistAndSongName] @Artist, @Title", artistParam, titleParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadTrackByIdReturnModel> SpReadTrackById(int? id)
        {
            int procResult;
            return SpReadTrackById(id, out procResult);
        }

        public List<SpReadTrackByIdReturnModel> SpReadTrackById(int? id, out int procResult)
        {
            var idParam = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = id.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!id.HasValue)
                idParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadTrackByIdReturnModel>("EXEC @procResult = [dbo].[sp_ReadTrackById] @Id", idParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadTrackByIdReturnModel>> SpReadTrackByIdAsync(int? id)
        {
            var idParam = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = id.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!id.HasValue)
                idParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadTrackByIdReturnModel>("EXEC [dbo].[sp_ReadTrackById] @Id", idParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadTrackIsrcReturnModel> SpReadTrackIsrc(string isrc)
        {
            int procResult;
            return SpReadTrackIsrc(isrc, out procResult);
        }

        public List<SpReadTrackIsrcReturnModel> SpReadTrackIsrc(string isrc, out int procResult)
        {
            var isrcParam = new SqlParameter { ParameterName = "@ISRC", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = isrc, Size = 50 };
            if (isrcParam.Value == null)
                isrcParam.Value = System.DBNull.Value;

            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadTrackIsrcReturnModel>("EXEC @procResult = [dbo].[sp_ReadTrackISRC] @ISRC", isrcParam, procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadTrackIsrcReturnModel>> SpReadTrackIsrcAsync(string isrc)
        {
            var isrcParam = new SqlParameter { ParameterName = "@ISRC", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, Value = isrc, Size = 50 };
            if (isrcParam.Value == null)
                isrcParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<SpReadTrackIsrcReturnModel>("EXEC [dbo].[sp_ReadTrackISRC] @ISRC", isrcParam).ToListAsync();

            return procResultData;
        }

        public List<SpReadTracksReturnModel> SpReadTracks()
        {
            int procResult;
            return SpReadTracks(out procResult);
        }

        public List<SpReadTracksReturnModel> SpReadTracks(out int procResult)
        {
            var procResultParam = new SqlParameter { ParameterName = "@procResult", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };
            var procResultData = Database.SqlQuery<SpReadTracksReturnModel>("EXEC @procResult = [dbo].[sp_ReadTracks] ", procResultParam).ToList();

            procResult = (int)procResultParam.Value;
            return procResultData;
        }

        public async Task<List<SpReadTracksReturnModel>> SpReadTracksAsync()
        {
            var procResultData = await Database.SqlQuery<SpReadTracksReturnModel>("EXEC [dbo].[sp_ReadTracks] ").ToListAsync();

            return procResultData;
        }

    }
}
