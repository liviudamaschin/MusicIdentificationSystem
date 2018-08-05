﻿using MusicIdentification.Core;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.GetResults
{
    public class MatchResults
    {
        protected static readonly int Maxthreads = 5;

        protected static int BatchActiveThreadsCount = 0;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MatchResults));

        public MatchResults()
        {
          
        }

        public static void MatchResultsFromStream()
        {
            string retFile = string.Empty;
            double confidfence = Convert.ToDouble(cApp.AppSettings["Confidfence"]);
            log.Info("StartMatching flow...");
            log.Info($"Max threads:{MatchResults.Maxthreads}");
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            StreamRepository streamRepository = new StreamRepository();
            //TrackRepository trackRepository = new TrackRepository();
            //ResultRepository resultRepository = new ResultRepository();
            var unprocessedStreamStations = streamRepository.GetUnprocessedStreams();
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
                            log.Info($"Lounch processing for: {unprocessedStreamStation.ProcessFile}");
                            Task<bool> task = ProcessFileForResults(unprocessedStreamStation.ProcessFile, confidfence, unprocessedStreamStation.Id);
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
                            MatchResults.BatchActiveThreadsCount--;
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
                    log.Error($"eroarea: { JsonConvert.SerializeObject(ex)}");
                    throw;
                }
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
            //StreamEntity unprocessedStream = streamRepository.GetByID(streamId);
            //unprocessedStream.ProcessDate = DateTime.Now;
            //streamRepository.Update(unprocessedStream);
            //streamRepository.Save();
            return true;
        }

        public static void MatchStream()
        {
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            StreamRepository streamRepository = new StreamRepository();
            TrackRepository trackRepository = new TrackRepository();
            ResultRepository resultRepository = new ResultRepository();
            double confidfence = Convert.ToDouble(cApp.AppSettings["Confidfence"]);
            var unprocessedStreamStations = streamRepository.GetUnprocessedStreams();
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
