using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicIdentificationSystem.StreamCapture
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(RadioCapture));
        static void Main(string[] args)
        {
            //Console.WriteLine(cApp.AppSettings["StreamPath"]);
            RecordStations();
        }

        private static void RecordStations()
        {

            #region taskProcessing
            
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            
            // Instantiate the CancellationTokenSource.
            //cts = new CancellationTokenSource();
            try
            {
                Dictionary<int, CancellationTokenSource> runningStations = new Dictionary<int, CancellationTokenSource>();
                while (true)
                {
                    StreamStationRepository streamStationRepository = new StreamStationRepository();
                    var streamStations = streamStationRepository.GetList();
                    //ForEach()
                    foreach (var station in streamStations)
                    {
                        if (station.IsActive)
                        {

                            if (!runningStations.ContainsKey(station.Id))
                            {
                                RadioCapture capture = new RadioCapture();
                                Console.WriteLine("Launch capture station {0}", station.StationName);
                                log.Info($"Launch capture station { station.StationName}");
                                CancellationTokenSource cts = new CancellationTokenSource();
                                Task<bool> task = capture.StartCapture2(cts.Token, 0, station);
                                runningStations.Add(station.Id, cts);
                                StreamStationEntity streamStation = streamStationRepository.GetByID(station.Id);
                                streamStation.Running = true;
                                streamStationRepository.Update(streamStation);
                                streamStationRepository.Save();

                                Console.WriteLine("End launch capture station {0}", station.StationName);
                                log.Info($"End launch capture station {station.StationName}");
                            }
                            else
                            {
                                if (station.Running == false)
                                {
                                    runningStations[station.Id].Cancel();
                                    runningStations.Remove(station.Id);
                                    StreamStationEntity streamStation = streamStationRepository.GetByID(station.Id);
                                    streamStation.Running = false;
                                    streamStationRepository.Update(streamStation);
                                    streamStationRepository.Save();
                                    Console.WriteLine("End capture station {0}", station.StationName);
                                    log.Info($"End capture station {station.StationName}");
                                }
                            }
                        }
                        else
                        {
                            if (runningStations.ContainsKey(station.Id))
                            {
                                runningStations[station.Id].Cancel();
                                runningStations.Remove(station.Id);
                                Console.WriteLine("End capture station {0}", station.StationName);
                                log.Info($"End capture station {station.StationName}");
                            }
                        }
                    }
                    //unitOfWork.DisposeDbContext();
                    streamStations = streamStationRepository.Get();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.InnerException.Message);
                log.Error($"Error: {JsonConvert.SerializeObject(ex)}");
                
                throw;
            }
           
            #endregion

        }
    }
}
