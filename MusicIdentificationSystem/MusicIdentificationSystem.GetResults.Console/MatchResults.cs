using MusicIdentification.Core;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.GetResults.Console
{
    public class MatchResults
    {
        protected static readonly int Maxthreads = 1;

        protected static int BatchActiveThreadsCount = 0;

        public static void MatchResultsFromStream()
        {
            string retFile = string.Empty;
            double confidfence = Convert.ToDouble(cApp.AppSettings["Confidfence"]);
            
            UnitOfWork2 unitOfWork = new UnitOfWork2();
            var unprocessedStreamStations = unitOfWork.StreamRepository.GetUnprocessedStreams();
            foreach (var unprocessedStreamStation in unprocessedStreamStations)
            {
                try
                {
                    while (MatchResults.BatchActiveThreadsCount >= MatchResults.Maxthreads)
                    {
                        Thread.Sleep(100);
                    }
                    MatchResults.BatchActiveThreadsCount++;
                    if (unprocessedStreamStation.ProcessFile != null)
                    {
                        Fingerprint fingerprint = new Fingerprint();
                        new Task(() =>
                        {
                            List<ResultEntity> results = fingerprint.GetBestMatchForSong(Path.Combine(cApp.AppSettings["StreamPath"], unprocessedStreamStation.ProcessFile), confidfence);
                            //save results to db
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

        public static void MatchStream()
        {
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            StreamRepository streamRepository = new StreamRepository();
            TrackRepository trackRepository = new TrackRepository();
            ResultRepository resultRepository = new ResultRepository();
            double confidfence = Convert.ToDouble(cApp.AppSettings["Confidfence"]);
            var unprocessedStreamStations = streamRepository.GetUnprocessedStreams();
            foreach (var unprocessedStreamStation in unprocessedStreamStations)
            {
                if (unprocessedStreamStation.ProcessFile != null)
                {
                    Fingerprint fingerprint = new Fingerprint();
                    List<ResultEntity> results = fingerprint.GetBestMatchForSong(Path.Combine(cApp.AppSettings["StreamPath"], unprocessedStreamStation.ProcessFile), confidfence);
                    foreach (var result in results)
                    {
                        var track = trackRepository.GetByID(result.TrackId);

                        ResultEntity resultEntity = new ResultEntity();
                        resultEntity.StreamId = unprocessedStreamStation.Id;
                        resultEntity.TrackId = result.TrackId;
                        resultEntity.Filename = unprocessedStreamStation.ProcessFile;
                        resultEntity.MatchStartAt = result.MatchStartAt;

                        resultRepository.Insert(resultEntity);
                        resultRepository.Save();
 
                    }
                }
                StreamEntity unprocessedStream = streamRepository.GetByID(unprocessedStreamStation.Id);
                unprocessedStream.ProcessDate = DateTime.Now;
                streamRepository.Update(unprocessedStream);
                streamRepository.Save();
            }
        }
    }
}
