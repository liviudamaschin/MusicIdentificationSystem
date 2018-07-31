using MusicIdentificationSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public class ReportsRepository : GenericRepository2<Sp_TimeReport>
    {
        public List<Sp_TimeReport> GetTimeReport(DateTime startDate,DateTime endDate,string accountIds, string streamStationIds, string trackIds,int reportType)
        {
            List<Sp_TimeReport> sp_TimeReportList;

            var startDateParameter = new SqlParameter("@StartDate", startDate)
            {
                IsNullable = false,
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.DateTime
            };

            var endDateParameter = new SqlParameter("@EndDate", endDate)
            {
                IsNullable = false,
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.DateTime
            };

            SqlParameter accountIdsParameter;
            if (string.IsNullOrEmpty(accountIds))
                accountIdsParameter = new SqlParameter("@AccountIds", DBNull.Value)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Text
                };
            else
                accountIdsParameter = new SqlParameter("@AccountIds", accountIds)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Text
                };

            SqlParameter streamStationIdsParameter;
            if (string.IsNullOrEmpty(streamStationIds))
                streamStationIdsParameter = new SqlParameter("@StreamStationIds", DBNull.Value)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Text
                };
            else
                streamStationIdsParameter = new SqlParameter("@StreamStationIds", accountIds)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Text
                };

            SqlParameter trackIdsParameter;
            if (string.IsNullOrEmpty(trackIds))
                trackIdsParameter = new SqlParameter("@TrackIds", DBNull.Value)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Text
                };
            else
                trackIdsParameter = new SqlParameter("@TrackIds", accountIds)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Text
                };

            var reportTypeParameter = new SqlParameter("@ReportType", reportType)
            {
                IsNullable = false,
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int
            };

            sp_TimeReportList = this.ExecuteProcedure<Sp_TimeReport>("sp_TimeReport", startDateParameter, endDateParameter, accountIdsParameter, streamStationIdsParameter, trackIdsParameter, reportTypeParameter).ToList();

            return sp_TimeReportList;
        }
    }
}
