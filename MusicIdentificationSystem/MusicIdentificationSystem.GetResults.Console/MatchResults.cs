using MusicIdentification.Core;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.GetResults.Console
{
    public class MatchResults
    {
        protected static readonly int Maxthreads = 2;

        protected static int BatchActiveThreadsCount = 0;

        private static void MatchResultsFromStream()
        {
            string retFile = string.Empty;
            while (MatchResults.BatchActiveThreadsCount >= MatchResults.Maxthreads)
            {
                Thread.Sleep(100);
            }
            UnitOfWork2 unitOfWork = new UnitOfWork2();
            var unprocessedStreamStations = unitOfWork.StreamStationRepository.GetUnprocessedStreams();
            foreach (var unprocessedStreamStation in unprocessedStreamStations)
            {
                try
                {
                    MatchResults.BatchActiveThreadsCount++;
                    if (unprocessedStreamStation.FileNameTransformed != null)
                    {
                        Fingerprint fingerprint = new Fingerprint();
                        new Task(() =>
                        {
                            List<ResultEntity> results = fingerprint.GetMatchSongsFromFolder(unprocessedStreamStation.FileNameTransformed);
                            //safe results to db
                            foreach (var result in results)
                            {
                                var track = unitOfWork.TrackRepository.GetByID(result.TrackId);
                                StreamResultsEntity streamResult = new StreamResultsEntity();

                            }
                            MatchResults.BatchActiveThreadsCount--;
                        }).Start();
                    }
                       
                }
                catch (Exception)
                {

                    throw;
                }
            }
            //return retFile;
        }
        private static void MatchStream()
        {
            UnitOfWork2 unitOfWork = new UnitOfWork2();
            var unprocessedStreamStations = unitOfWork.StreamStationRepository.GetUnprocessedStreams();
            foreach (var unprocessedStreamStation in unprocessedStreamStations)
            {
                if (unprocessedStreamStation.FileNameTransformed != null)
                {
                    Fingerprint fingerprint = new Fingerprint();
                    List<ResultEntity> results = fingerprint.GetMatchSongsFromFolder(unprocessedStreamStation.FileNameTransformed);
                    foreach (var result in results)
                    {
                        var track = unitOfWork.TrackRepository.GetByID(result.TrackId);
                        StreamResultsEntity streamResult = new StreamResultsEntity();

                    }
                }
            }
        }
    }
}
