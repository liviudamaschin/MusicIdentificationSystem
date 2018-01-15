using MusicIdentificationSystem.EF.Context;
using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL
{
    public static class StreamStation    {
        public static bool AddStreamStation(StreamStationEntity entity) {
            int changesSaved=0;
            using (var dbContext = new Db())
            {
                var streamStation = new StreamStationEntity
                {
                    StationName = entity.StationName,
                    Url = entity.Url,
                    Description = entity.Description,
                    IsActive = entity.IsActive,
                    TransformFolder=entity.TransformFolder
                };
                dbContext.StreamStations.Add(streamStation);
                changesSaved = dbContext.SaveChanges();
            }
            return changesSaved >= 1;
        }

        public static BindingList<StreamStationEntity> GetStreamStations()
        {
            BindingList<StreamStationEntity> stations = new BindingList<StreamStationEntity>();
            using (var db = new Db())
            {
                foreach (StreamStationEntity entity in db.StreamStations)
                {
                    stations.Add(entity);
                }
            }

            return stations;
        }
    }
}
