using MusicIdentificationSystem.DAL.Context;
using MusicIdentificationSystem.DAL.DatabaseConfiguration;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using StructureMap;
using System;

namespace MusicIdentificationSystem.DAL.UnitOfWork
{
    public class UnitOfWork2 : IDisposable
    {
        private DatabaseContext context;
        private GenericRepository2<FingerprintEntity> fingerprintRepository;
        private ResultRepository resultRepository;
        private StreamRepository streamRepository;
        private StreamStationRepository streamStationRepository;
        private GenericRepository2<SubFingerprintEntity> subFingerprintRepository;
        private TrackRepository trackRepository;
        private AccountRepository accountRepository;
        private GenericRepository2<AccountStreamStationEntity> accountStreamStationRepository;
        private ApplicationSettingRepository applicationSettingRepository;
        private ClientRepository clientRepository;
        private GenericRepository2<StreamResultsEntity> streamResultsRepository;

        private readonly IContainer container;

        private IDatabaseConfigurationManager config;
        public UnitOfWork2()
        {
            //this.config = config;
            this.container = new Container();
            //this.context = new DatabaseContext();


        }

        private DatabaseContext SetContext()
        {
            return this.container.GetInstance<DatabaseContext>();
        }

        //public GenericRepository<FingerprintEntity> FingerprintRepository
        //{
        //    get
        //    {
        //        return this.fingerprintRepository ?? new GenericRepository<FingerprintEntity>(context);
        //    }
        //}
        
        public ResultRepository ResultRepository
        {
            get
            {
                return this.resultRepository ?? new ResultRepository();
            }
        }
        
        public StreamRepository StreamRepository
        {
            get
            {
                return this.streamRepository ?? new StreamRepository();
            }
        }

        public StreamStationRepository StreamStationRepository
        {
            get
            {
                return this.streamStationRepository ?? new StreamStationRepository();
            }
        }
        //public GenericRepository<SubFingerprintEntity> SubFingerprintRepository
        //{
        //    get
        //    {
        //        return this.subFingerprintRepository ?? new GenericRepository<SubFingerprintEntity>(context);
        //    }
        //}
        public TrackRepository TrackRepository
        {
            get
            {
                return this.trackRepository ?? new TrackRepository();
            }
        }
        public AccountRepository AccountRepository
        {
            get
            {
                return this.accountRepository ?? new AccountRepository();
            }
        }
        //public GenericRepository<AccountStreamStationEntity> AccountStreamStationRepository
        //{
        //    get
        //    {
        //        return this.accountStreamStationRepository ?? new GenericRepository<AccountStreamStationEntity>(context);
        //    }
        //}
        public ApplicationSettingRepository ApplicationSettingRepository
        {
            get
            {
                return this.applicationSettingRepository ?? new ApplicationSettingRepository();
            }
        }
        public ClientRepository ClientRepository
        {
            get
            {
                return this.clientRepository ?? new ClientRepository();
            }
        }
        //public GenericRepository<StreamResultsEntity> StreamResultsRepository
        //{
        //    get
        //    {
        //        return this.streamResultsRepository ?? new GenericRepository<StreamResultsEntity>(context);
        //    }
        //}

        //public List<SpMisGetActiveStationsReturnModel> GetActiveStations()
        //{
        //    return context.SpMisGetActiveStations();
        //}

        //public List<SpGetUnprocessedStreamsReturnModel> GetUnprocessedStreams()
        //{
        //    return context.SpGetUnprocessedStreams();
        //}

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DisposeDbContext()
        {
            context.Dispose();
            this.context = new DatabaseContext();
        }
    }
}
