using AutoMapper;
using MusicIdentificationSystem.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public class StreamStationVideoRepository : GenericRepository2<StreamStationVideoEntity>
    {
        private readonly IMapper mapper;

        //public List<ActiveVideStation> GetGetActiveStations()
        //{
        //    List<ActiveVideStation> streamStationEntityList;

        //    streamStationEntityList = this.ExecuteProcedure<ActiveVideStation>("sp_mis_GetActiveStations").ToList();

        //    return streamStationEntityList;
        //}
    }
}
