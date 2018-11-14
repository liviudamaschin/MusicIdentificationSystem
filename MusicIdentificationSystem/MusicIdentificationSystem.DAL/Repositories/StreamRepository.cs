using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public class StreamRepository : GenericRepository2<StreamEntity>
    {
        public List<UnprocessedStreams> GetUnprocessedStreams(int maxRecords)
        {
            List<UnprocessedStreams> streamStationEntityList;
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@maxRecords",
                SqlValue = maxRecords,
                DbType = DbType.Int32
            };
            streamStationEntityList = this.ExecuteProcedure<UnprocessedStreams>("sp_GetUnprocessedStreams", param).ToList();


            return streamStationEntityList;
        }

        public UnprocessedStreams GetOldestUnprocessedStream(int rank)
        {
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@rank",
                SqlValue = rank,
                DbType = DbType.Int32
            };

            var streamStationEntity = this.ExecuteProcedure<UnprocessedStreams>("sp_GetOldestUnprocessedStream", param).FirstOrDefault();


            return streamStationEntity;
        }

        public List<UnprocessedStreams> GetUnconvertedStreams()
        {
            List<UnprocessedStreams> streamStationEntityList;
            streamStationEntityList = this.ExecuteProcedure<UnprocessedStreams>("sp_GetUnconvertedStreams").ToList();


            return streamStationEntityList;
        }
    }
}
