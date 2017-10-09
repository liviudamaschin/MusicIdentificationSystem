using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicIdentificationSystem.Models;

namespace MusicIdentificationSistem.DAL
{
    public interface IStreamStationsRepository : IDisposable
    {
        IEnumerable<StreamStationModel> GetStreamStations();
        StreamStationModel GetStreamStationByID(int streamStationId);
        void InsertStreamStation(StreamStationModel streamStation);
        void DeleteStreamStationt(int studentID);
        void UpdateStreamStation(StreamStationModel streamStation);
        void Save();
    }
}
