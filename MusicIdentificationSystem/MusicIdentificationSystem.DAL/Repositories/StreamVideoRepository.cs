using MusicIdentificationSystem.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public class StreamVideoRepository : GenericRepository2<StreamVideoEntity>
    {
        //public List<UnprocessedStreams> GetUnprocessedStreams()
        //{
        //    List<UnprocessedStreams> streamStationEntityList;
        //    streamStationEntityList = this.ExecuteProcedure<UnprocessedStreams>("sp_GetUnprocessedStreams").ToList();


        //    return streamStationEntityList;
        //}

        //public List<UnprocessedStreams> GetUnconvertedStreams()
        //{
        //    List<UnprocessedStreams> streamStationEntityList;
        //    streamStationEntityList = this.ExecuteProcedure<UnprocessedStreams>("sp_GetUnconvertedStreams").ToList();


        //    return streamStationEntityList;
        //}
    }
}
