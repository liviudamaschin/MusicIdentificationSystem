using MusicIdentificationSystem.EF.Context;
using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
        private GenericRepository<TrackEntity> trackRepository;

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
        public GenericRepository<TrackEntity> TrackRepository
        {
            get
            {
                return this.trackRepository ?? new GenericRepository<TrackEntity>(context);
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

        public void DisposeDbContext()
        {
            context.Dispose();
            context = new Db();
        }
    }
}
