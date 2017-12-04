using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.Dashboard.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var activeStreamStations = unitOfWork.GetActiveStations();
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
                            StreamStationEntity streamStation = unitOfWork.StreamStationRepository.GetByID(activeStation.Id);
                            if (fs.Length <= runningStations[activeStation.Id])
                            {
                                //not working (something wrong)
                                streamStation.Running = false;
                            }
                            else
                            {
                                streamStation.Running = true;
                            }
                            unitOfWork.StreamStationRepository.Update(streamStation);
                            unitOfWork.Save();
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
