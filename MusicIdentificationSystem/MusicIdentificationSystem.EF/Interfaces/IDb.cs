using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.EF.Interfaces
{
    public interface IDb : IDisposable
    {
        DbSet<AccountEntity> Accounts { get; set; } // Accounts
        DbSet<AccountStreamStationEntity> AccountStreamStations { get; set; } // AccountStreamStations
        DbSet<ApplicationSettingEntity> ApplicationSettings { get; set; } // ApplicationSettings
        DbSet<ClientEntity> Clients { get; set; } // Clients
        DbSet<FingerprintEntity> Fingerprints { get; set; } // Fingerprints
        DbSet<ResultEntity> Results { get; set; } // Results
        DbSet<StreamEntity> Streams { get; set; } // Stream
        DbSet<StreamStationEntity> StreamStations { get; set; } // StreamStations
        DbSet<SubFingerprintEntity> SubFingerprints { get; set; } // SubFingerprints
        DbSet<TrackEntity> Tracks { get; set; } // Tracks

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        Database Database { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
        IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();

        // Stored Procedures
        int SpDeleteTrack(int? id);
        // SpDeleteTrackAsync cannot be created due to having out parameters, or is relying on the procedure result (int)

       List<SpInsertFingerprintReturnModel> SpInsertFingerprint(byte[] signature, int? trackId);
       List<SpInsertFingerprintReturnModel> SpInsertFingerprint(byte[] signature, int? trackId, out int procResult);
        Task<List<SpInsertFingerprintReturnModel>> SpInsertFingerprintAsync(byte[] signature, int? trackId);

       List<SpInsertSubFingerprintReturnModel> SpInsertSubFingerprint(int? trackId, int? sequenceNumber, double? sequenceAt, long? hashTable0, long? hashTable1, long? hashTable2, long? hashTable3, long? hashTable4, long? hashTable5, long? hashTable6, long? hashTable7, long? hashTable8, long? hashTable9, long? hashTable10, long? hashTable11, long? hashTable12, long? hashTable13, long? hashTable14, long? hashTable15, long? hashTable16, long? hashTable17, long? hashTable18, long? hashTable19, long? hashTable20, long? hashTable21, long? hashTable22, long? hashTable23, long? hashTable24, string clusters);
       List<SpInsertSubFingerprintReturnModel> SpInsertSubFingerprint(int? trackId, int? sequenceNumber, double? sequenceAt, long? hashTable0, long? hashTable1, long? hashTable2, long? hashTable3, long? hashTable4, long? hashTable5, long? hashTable6, long? hashTable7, long? hashTable8, long? hashTable9, long? hashTable10, long? hashTable11, long? hashTable12, long? hashTable13, long? hashTable14, long? hashTable15, long? hashTable16, long? hashTable17, long? hashTable18, long? hashTable19, long? hashTable20, long? hashTable21, long? hashTable22, long? hashTable23, long? hashTable24, string clusters, out int procResult);
        Task<List<SpInsertSubFingerprintReturnModel>> SpInsertSubFingerprintAsync(int? trackId, int? sequenceNumber, double? sequenceAt, long? hashTable0, long? hashTable1, long? hashTable2, long? hashTable3, long? hashTable4, long? hashTable5, long? hashTable6, long? hashTable7, long? hashTable8, long? hashTable9, long? hashTable10, long? hashTable11, long? hashTable12, long? hashTable13, long? hashTable14, long? hashTable15, long? hashTable16, long? hashTable17, long? hashTable18, long? hashTable19, long? hashTable20, long? hashTable21, long? hashTable22, long? hashTable23, long? hashTable24, string clusters);

       List<SpInsertTrackReturnModel> SpInsertTrack(string isrc, string artist, string title, string album, int? releaseYear, double? length);
       List<SpInsertTrackReturnModel> SpInsertTrack(string isrc, string artist, string title, string album, int? releaseYear, double? length, out int procResult);
        Task<List<SpInsertTrackReturnModel>> SpInsertTrackAsync(string isrc, string artist, string title, string album, int? releaseYear, double? length);

       List<SpReadFingerprintByTrackIdReturnModel> SpReadFingerprintByTrackId(int? trackId);
       List<SpReadFingerprintByTrackIdReturnModel> SpReadFingerprintByTrackId(int? trackId, out int procResult);
        Task<List<SpReadFingerprintByTrackIdReturnModel>> SpReadFingerprintByTrackIdAsync(int? trackId);

       List<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel> SpReadFingerprintsByHashBinHashTableAndThreshold(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold);
       List<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel> SpReadFingerprintsByHashBinHashTableAndThreshold(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, out int procResult);
        Task<List<SpReadFingerprintsByHashBinHashTableAndThresholdReturnModel>> SpReadFingerprintsByHashBinHashTableAndThresholdAsync(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold);

       List<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel> SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, string clusters);
       List<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel> SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClusters(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, string clusters, out int procResult);
        Task<List<SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersReturnModel>> SpReadSubFingerprintsByHashBinHashTableAndThresholdWithClustersAsync(long? hashBin0, long? hashBin1, long? hashBin2, long? hashBin3, long? hashBin4, long? hashBin5, long? hashBin6, long? hashBin7, long? hashBin8, long? hashBin9, long? hashBin10, long? hashBin11, long? hashBin12, long? hashBin13, long? hashBin14, long? hashBin15, long? hashBin16, long? hashBin17, long? hashBin18, long? hashBin19, long? hashBin20, long? hashBin21, long? hashBin22, long? hashBin23, long? hashBin24, int? threshold, string clusters);

       List<SpReadSubFingerprintsByTrackIdReturnModel> SpReadSubFingerprintsByTrackId(int? trackId);
       List<SpReadSubFingerprintsByTrackIdReturnModel> SpReadSubFingerprintsByTrackId(int? trackId, out int procResult);
        Task<List<SpReadSubFingerprintsByTrackIdReturnModel>> SpReadSubFingerprintsByTrackIdAsync(int? trackId);

       List<SpReadTrackByArtistAndSongNameReturnModel> SpReadTrackByArtistAndSongName(string artist, string title);
       List<SpReadTrackByArtistAndSongNameReturnModel> SpReadTrackByArtistAndSongName(string artist, string title, out int procResult);
        Task<List<SpReadTrackByArtistAndSongNameReturnModel>> SpReadTrackByArtistAndSongNameAsync(string artist, string title);

       List<SpReadTrackByIdReturnModel> SpReadTrackById(int? id);
       List<SpReadTrackByIdReturnModel> SpReadTrackById(int? id, out int procResult);
        Task<List<SpReadTrackByIdReturnModel>> SpReadTrackByIdAsync(int? id);

       List<SpReadTrackIsrcReturnModel> SpReadTrackIsrc(string isrc);
       List<SpReadTrackIsrcReturnModel> SpReadTrackIsrc(string isrc, out int procResult);
        Task<List<SpReadTrackIsrcReturnModel>> SpReadTrackIsrcAsync(string isrc);

       List<SpReadTracksReturnModel> SpReadTracks();
       List<SpReadTracksReturnModel> SpReadTracks(out int procResult);
        Task<List<SpReadTracksReturnModel>> SpReadTracksAsync();
    }
}
