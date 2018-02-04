using AutoMapper;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MusicIdentificationSystem.Dashboard.Console
{
    public class Monitor
    {
        private readonly StreamStationRepository streamStationRepository2;
        private readonly IMapper mapper;
        StreamStationRepository streamStationRepository = new StreamStationRepository();
        //private readonly UnitOfWork2 unitOfWork;
        public Monitor()
        {
            //this.streamStationRepository2 = unitOfWorkStreamStations.Repository;
            //this.mapper = mapper;
            //this.unitOfWork = new UnitOfWork2();
        }

        public void StartMonitoring()
        {
            //StreamStationRepository2 streamStationRepository = new StreamStationRepository2();
            var activeStreamStations = streamStationRepository.GetGetActiveStations();
            Dictionary<int, long> runningStations = new Dictionary<int, long>();
            while (true)
            {
                foreach (var activeStation in activeStreamStations)
                {
                    if (activeStation.IsActive.HasValue && activeStation.IsActive.Value)
                    {
                        Stream fs = File.Open(activeStation.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        if (runningStations.ContainsKey(activeStation.Id))
                        {
                            StreamStationEntity streamStation = streamStationRepository.GetByID(activeStation.Id);
                            if (fs.Length <= runningStations[activeStation.Id])
                            {
                                //not working (something wrong)
                                streamStation.Running = false;
                            }
                            else
                            {
                                streamStation.Running = true;
                            }
                            streamStationRepository.Update(streamStation);
                            streamStationRepository.Save();
                            runningStations[activeStation.Id] = fs.Length;
                        }
                        else
                        {
                            runningStations.Add(activeStation.Id, fs.Length);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
