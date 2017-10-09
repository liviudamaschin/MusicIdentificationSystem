using MusicIdentificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSistem.DAL
{
    public class StreamStationsRepository
    {
        private MusicIdentificatoinContext context;

        public StreamStationsRepository(MusicIdentificatoinContext context)
        {
            this.context = context;
        }

        public IEnumerable<StreamStationModel> GetStreamStations()
        {
            return context.StreamStations.ToList();
        }

        public StreamStationModel GetStreamStationByID(int id)
        {
            return context.StreamStations.Find(id);
        }

        public void InsertStreamStation(StreamStationModel streamStation)
        {
            context.StreamStations.Add(streamStation);
        }

        public void DeleteStreamStation(int streamStationId)
        {
            StreamStationModel streamStation = context.StreamStations.Find(streamStationId);
            context.StreamStations.Remove(streamStation);
        }

        public void UpdateStreamStation(StreamStationModel streamStation)
        {
            context.Entry(streamStation).State = System.Data.Entity.EntityState.Modified;
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
