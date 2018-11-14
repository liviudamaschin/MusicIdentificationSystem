using MusicIdentification.Core;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MusicIdentificationSystem.DTO;

namespace MusicIdentificationSystem.GetResults
{
    public class MatchResults
    {
        //protected static int Maxthreads = 4;

        protected static int BatchActiveThreadsCount = 0;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MatchResults));

        public MatchResults()
        {
          
        }

        public static void MatchResultsFromStream()
        {
            string retFile = string.Empty;
            double confidence = Convert.ToDouble(cApp.AppSettings["Confidence"]);
            log.Info("StartMatching flow...");
            //MatchResults.Maxthreads = Convert.ToInt32(cApp.AppSettings["MaxThreads"]);
            log.Info($"Max threads:{cApp.AppSettings["MaxThreads"]}");
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            StreamRepository streamRepository = new StreamRepository();
            //TrackRepository trackRepository = new TrackRepository();
            //ResultRepository resultRepository = new ResultRepository();
            //var unprocessedStreamStations = streamRepository.GetUnprocessedStreams()
            List<string> pendingMatching = new List<string>();
            var rank = 1;
            try
            {
                while (true)
                {
                    var maxThreads = Convert.ToInt32(cApp.AppSettings["MaxThreads"]);
                    rank = 1;
                    var unprocessedStreamStation = streamRepository.GetOldestUnprocessedStream(rank);
                    if (unprocessedStreamStation == null)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    var notExist = false;
                    while (unprocessedStreamStation == null || pendingMatching.Contains(unprocessedStreamStation.ProcessFile))
                    {
                        if (unprocessedStreamStation != null)
                            rank++;

                        unprocessedStreamStation = streamRepository.GetOldestUnprocessedStream(rank);
                        //if (unprocessedStreamStation == null)
                        //{
                        //    Thread.Sleep(1000);
                        //notExist = true;
                        //}
                    }
                    log.Info($"Max threads:{maxThreads}");
                    //var unprocessedStreamStations = streamRepository.GetUnprocessedStreams(50);
                    //while (!unprocessedStreamStations.Any())
                    //{
                    //    unprocessedStreamStations = streamRepository.GetUnprocessedStreams(50);
                    //}

                    //foreach (var unprocessedStreamStation in unprocessedStreamStations)
                    if (unprocessedStreamStation != null)
                    {
                        //if (pendingMatching.Contains(unprocessedStreamStation.ProcessFile))
                        //{
                        //    rank++;
                        //    continue;
                        //}

                        //rank--;
                        pendingMatching.Add(unprocessedStreamStation.ProcessFile);
                        try
                        {
                            while (MatchResults.BatchActiveThreadsCount >= maxThreads)
                            {
                                Thread.Sleep(100);
                            }

                            MatchResults.BatchActiveThreadsCount++;
                            if (unprocessedStreamStation.ProcessFile != null)
                            {
                                Fingerprint fingerprint = new Fingerprint();
                                new Task(() =>
                                {
                                    log.Info($"Launch processing for: {unprocessedStreamStation.ProcessFile}");
                                    Task<bool> task = ProcessFileForResults(unprocessedStreamStation.ProcessFile,
                                        confidence, unprocessedStreamStation.Id);
                                    //List<ResultEntity> results = fingerprint.GetBestMatchForSong(Path.Combine(cApp.AppSettings["StreamPath"], unprocessedStreamStation.ProcessFile), confidfence);
                                    //log.Info($"results for: {Newtonsoft.Json.JsonConvert.SerializeObject(results)}");
                                    ////save results to db
                                    //foreach (var result in results)
                                    //{
                                    //    var track = trackRepository.GetByID(result.TrackId);
                                    //    StreamResultsEntity streamResult = new StreamResultsEntity();
                                    //    ResultEntity resultEntity = new ResultEntity();
                                    //    resultEntity.StreamId = unprocessedStreamStation.Id;
                                    //    resultEntity.TrackId = result.TrackId;
                                    //    resultEntity.Filename = unprocessedStreamStation.ProcessFile;
                                    //    resultEntity.TrackMatchStartAt = result.TrackMatchStartAt;
                                    //    resultEntity.QueryMatchStartsAt = result.QueryMatchStartsAt;
                                    //    resultEntity.QueryMatchLength = result.QueryMatchLength;
                                    //    resultEntity.TrackStartsAt = result.TrackStartsAt;
                                    //    resultEntity.Coverage = result.Coverage;
                                    //    resultEntity.Confidence = result.Confidence;

                                    //    resultRepository.Insert(resultEntity);
                                    //    resultRepository.Save();
                                    //}
                                    //MatchResults.BatchActiveThreadsCount--;
                                }).Start();

                                //StreamEntity unprocessedStream = streamRepository.GetByID(unprocessedStreamStation.Id);
                                //unprocessedStream.ProcessDate = DateTime.Now;
                                //streamRepository.Update(unprocessedStream);
                                //streamRepository.Save();
                            }

                        }
                        catch (Exception ex)
                        {

                            log.Error(ex.InnerException.Message);
                            log.Error($"Error: {JsonConvert.SerializeObject(ex)}");
                            throw;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException.Message);
                log.Error($"Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
           
            log.Info("End matching flow...");
            //return retFile;
        }

        private static async Task<bool> ProcessFileForResults(string processFile, double confidence, int streamId)
        {
            TrackRepository trackRepository = new TrackRepository();
            ResultRepository resultRepository = new ResultRepository();
            StreamRepository streamRepository = new StreamRepository();
            Fingerprint fingerprint = new Fingerprint();
            List<ResultEntity> results = fingerprint.GetBestMatchForSong(Path.Combine(cApp.AppSettings["StreamPath"], processFile), confidence);
            if (results != null)
            {
                log.Info($"results for file: {processFile} count: {results.Count} {Environment.NewLine}");
                //save results to db
                foreach (var result in results)
                {
                    var track = trackRepository.GetByID(result.TrackId);
                    StreamResultsEntity streamResult = new StreamResultsEntity();
                    ResultEntity resultEntity = new ResultEntity();
                    resultEntity.StreamId = streamId;
                    resultEntity.TrackId = result.TrackId;
                    resultEntity.Filename = processFile;
                    resultEntity.TrackMatchStartAt = result.TrackMatchStartAt;
                    resultEntity.QueryMatchStartsAt = result.QueryMatchStartsAt;
                    resultEntity.QueryMatchLength = result.QueryMatchLength;
                    resultEntity.TrackStartsAt = result.TrackStartsAt;
                    resultEntity.Coverage = result.Coverage;
                    resultEntity.Confidence = result.Confidence;
                    log.Info($"  - result:{JsonConvert.SerializeObject(resultEntity)} {Environment.NewLine}");
                    resultRepository.Insert(resultEntity);
                    resultRepository.Save();
                }

                // remove comments only for testing
                StreamEntity unprocessedStream = streamRepository.GetByID(streamId);
                unprocessedStream.ProcessDate = DateTime.Now;
                unprocessedStream.Status = "SUCCESS";
                streamRepository.Update(unprocessedStream);
                streamRepository.Save();
            }
            else
            {
                log.Info($"results for file: {processFile} (NULL) {Environment.NewLine}");
                StreamEntity failStream = streamRepository.GetByID(streamId);
                failStream.Status = "FAIL";
                streamRepository.Update(failStream);
                streamRepository.Save();
            }

            MatchResults.BatchActiveThreadsCount--;
            return true;
        }

        public static void MatchStream()
        {
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            StreamRepository streamRepository = new StreamRepository();
            TrackRepository trackRepository = new TrackRepository();
            ResultRepository resultRepository = new ResultRepository();
            double confidfence = Convert.ToDouble(cApp.AppSettings["Confidfence"]);
            var unprocessedStreamStations = streamRepository.GetUnprocessedStreams(50);
            Console.WriteLine("Start matching streams:");
            log.Info("Start matching streams: ");
            foreach (var unprocessedStreamStation in unprocessedStreamStations)
            {
                Console.WriteLine($"matching  = {unprocessedStreamStation.FileName}");
                log.Info($"matching  = {unprocessedStreamStation.FileName}");
                if (unprocessedStreamStation.ProcessFile != null)
                {
                    Fingerprint fingerprint = new Fingerprint();
                    List<ResultEntity> results = fingerprint.GetBestMatchForSong(Path.Combine(cApp.AppSettings["StreamPath"], unprocessedStreamStation.ProcessFile), confidfence);
                    Console.WriteLine($"matched results:  = {results.Count}");
                    log.Info($"matched results:  = {results.Count}");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"result:  = {result.TrackId.Value}.{result.Track?.Artist} - {result.Track?.Title}");
                        log.Info($"result:  = {result.TrackId.Value}.{result.Track?.Artist} - {result.Track?.Title}");
                        var track = trackRepository.GetByID(result.TrackId);

                        ResultEntity resultEntity = new ResultEntity();
                        resultEntity.StreamId = unprocessedStreamStation.Id;
                        resultEntity.TrackId = result.TrackId;
                        resultEntity.Filename = unprocessedStreamStation.ProcessFile;
                        resultEntity.TrackMatchStartAt = result.TrackMatchStartAt;
                        resultEntity.QueryMatchStartsAt =result.QueryMatchStartsAt;
                        resultEntity.QueryMatchLength = result.QueryMatchLength;
                        resultEntity.TrackStartsAt = result.TrackStartsAt;
                        resultEntity.Coverage = result.Coverage;
                        resultEntity.Confidence = result.Confidence;

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
