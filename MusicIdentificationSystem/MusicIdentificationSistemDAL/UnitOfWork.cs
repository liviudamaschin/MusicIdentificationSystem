using MusicIdentificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSistem.DAL
{
    public class UnitOfWork : IDisposable
    {
        private MusicIdentificatoinContext context = new MusicIdentificatoinContext();
        private GenericRepository<StreamStationModel> streamStationRepository;


        public GenericRepository<StreamStationModel> DepartmentRepository
        {
            get
            {

                if (this.streamStationRepository == null)
                {
                    this.streamStationRepository = new GenericRepository<StreamStationModel>(context);
                }
                return streamStationRepository;
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
