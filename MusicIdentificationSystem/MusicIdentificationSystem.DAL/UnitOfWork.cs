using MusicIdentificationSystem.EF.Context;
using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL
{
    public class UnitOfWork : IDisposable
    {
        private Db context = new Db();
        private GenericRepository<FingerprintEntity> fingerprintRepository;
        private GenericRepository<ResultEntity> resultRepository;
        private GenericRepository<StreamEntity> streamRepository;
        private GenericRepository<StreamStationEntity> streamStationRepository;
        private GenericRepository<SubFingerprintEntity> subFingerprintRepository;
        private GenericRepository<AccountEntity> accountRepository;
        private GenericRepository<AccountStreamStationEntity> accountStreamStationRepository;
        private GenericRepository<ApplicationSettingEntity> applicationSettingRepository;
        private GenericRepository<ClientEntity> clientRepository;

        public GenericRepository<FingerprintEntity> FingerprintRepository
        {
            get
            {
                return this.fingerprintRepository ?? new GenericRepository<FingerprintEntity>(context);
            }
        }
        public GenericRepository<ResultEntity> ResultRepository
        {
            get
            {
                return this.resultRepository ?? new GenericRepository<ResultEntity>(context);
            }
        }
        public GenericRepository<StreamEntity> StreamRepository
        {
            get
            {
                return this.streamRepository ?? new GenericRepository<StreamEntity>(context);
            }
        }
        public GenericRepository<StreamStationEntity> StreamStationRepository
        {
            get
            {
                return this.streamStationRepository ?? new GenericRepository<StreamStationEntity>(context);
            }
        }
        public GenericRepository<SubFingerprintEntity> SubFingerprintRepository
        {
            get
            {
                return this.subFingerprintRepository ?? new GenericRepository<SubFingerprintEntity>(context);
            }
        }
        public GenericRepository<AccountEntity> AccountRepository
        {
            get
            {
                return this.accountRepository ?? new GenericRepository<AccountEntity>(context);
            }
        }
        public GenericRepository<AccountStreamStationEntity> AccountStreamStationRepository
        {
            get
            {
                return this.accountStreamStationRepository ?? new GenericRepository<AccountStreamStationEntity>(context);
            }
        }
        public GenericRepository<ApplicationSettingEntity> ApplicationSettingRepository
        {
            get
            {
                return this.applicationSettingRepository ?? new GenericRepository<ApplicationSettingEntity>(context);
            }
        }
        public GenericRepository<ClientEntity> ClientRepository
        {
            get
            {
                return this.clientRepository ?? new GenericRepository<ClientEntity>(context);
            }
        }

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
    }
}
