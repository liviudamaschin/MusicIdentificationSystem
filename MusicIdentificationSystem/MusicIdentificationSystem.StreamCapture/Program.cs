using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.StreamCapture
{
    class Program
    {
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
                            Console.WriteLine("Lounch capture station {0}", station.StationName);
                            CancellationTokenSource cts = new CancellationTokenSource();
                            Task<bool> task = capture.StartCapture2(cts.Token, 0, station);
                            runningStations.Add(station.Id, cts);
                            StreamStationEntity streamStation = streamStationRepository.GetByID(station.Id);
                            streamStation.Running = true;
                            streamStationRepository.Update(streamStation);
                            streamStationRepository.Save();

                            Console.WriteLine("End lounch capture station {0}", station.StationName);
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
                        }
                    }
                }
                //unitOfWork.DisposeDbContext();
                streamStations = streamStationRepository.Get();
            }
            #endregion

        }
    }
}
