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
            RecordStations();
        }

        private static void RecordStations()
        {

            #region taskProcessing
            StreamStationRepository streamStationRepository = new StreamStationRepository();
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            var streamStations = streamStationRepository.Get();
            // Instantiate the CancellationTokenSource.
            //cts = new CancellationTokenSource();
            Dictionary<int, CancellationTokenSource> runningStations = new Dictionary<int, CancellationTokenSource>();
            while (true)
            {
                //ForEach()
                foreach (var station in streamStations)
                {
                    if (station.IsActive.HasValue && station.IsActive.Value)
                    {
                        if (!runningStations.ContainsKey(station.Id))
                        {
                            Capture capture = new Capture();
                            Console.WriteLine("Lounch capture station {0}", station.StationName);
                            CancellationTokenSource cts = new CancellationTokenSource();
                            Task<bool> task = capture.StartCapture2(cts.Token, 0, station);
                            runningStations.Add(station.Id, cts);
                            Console.WriteLine("End lounch capture station {0}", station.StationName);
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
