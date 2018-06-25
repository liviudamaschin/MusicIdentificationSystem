using MusicIdentificationSystem.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public class TrackRepository : GenericRepository2<TrackEntity>
    {
        public void ClearIsNewFromAccountTracks(int accountId)
        {
            var accountIdParameter = new SqlParameter("@AccountId", accountId)
            {
                IsNullable = false,
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int
            };

            this.ExecuteProcedure("sp_ClearIsNewFromAccountTracks", accountIdParameter);
        }
    }
}
