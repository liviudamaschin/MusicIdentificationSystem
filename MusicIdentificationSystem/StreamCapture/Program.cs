using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.EF.Context;
using MusicIdentificationSystem.EF.Entities;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreamCapture
{
    class Program
    {
        
        static void Main(string[] args)
        {

            RecordStations();
            //AudioDecoder audioDecoder = new AudioDecoder();
            //audioDecoder.ConvertMp3ToWavFolder(@"C:\Liviu\Stream\Paralel\rockfm\2017-10-01\", @"C:\Liviu\Stream\Paralel\rockfm\2017-10-01\out\");
            //audioDecoder.ConvertWavToMp3Folder(@"C:\Liviu\Stream\Paralel\rockfm\2017-10-01\out\", @"C:\Liviu\Stream\Paralel\rockfm\2017-10-01\out\");
        }
        private static void ConvertMp3ToWav_old(string _inPath_, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                }
            }
        }

        
        private static void ConcatFiles()
        {
            Capture capture = new Capture();
            capture.combineFolderContent(@"C:\Liviu\Stream\Paralel\rockfm\_2017-09-26\");
        }
        private static void RecordStations()
        {


            //var entity = new StreamStationEntity();
            //entity.Description = "A";
            //entity.StationName = "A";
            //entity.IsActive = false;
            //entity.Url = "A";

            //db.StreamStations.Add(entity);
            //db.SaveChanges();


            #region paralel processing
            //paralel processing
            //Parallel.ForEach(db.StreamStations, station =>
            //{
            //    if (station.IsActive.HasValue && station.IsActive.Value)
            //    {          
            //        Capture capture = new Capture();
            //        Console.WriteLine("Lounch capture station {0}", station.StationName);
            //        Task<bool> task = capture.StartCapture2(0, station);
            //        //capture.StartCapture2(0, station);
            //        //Console.WriteLine("End capture station {0}", station.StationName);
            //        ////Process process = new Process();
            //        //ProcessStartInfo startInfo = new ProcessStartInfo();
            //        ////startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //        //startInfo.FileName = "ffmpeg.exe";
            //        //startInfo.Arguments = String.Format(@"-y -t 60 -i {0} {1}DefaultRadio.mp3",station.Url, station.LocalPath);
            //        ////process.StartInfo = startInfo;
            //        ////process.Start();
            //        //try
            //        //{
            //        //    // Start the process with the info we specified.
            //        //    // Call WaitForExit and then the using statement will close.
            //        //    using (Process exeProcess = Process.Start(startInfo))
            //        //    {
            //        //        exeProcess.WaitForExit();
            //        //    }
            //        //}
            //        //catch
            //        //{
            //        //    // Log error.
            //        //}
            //        //string path = "ffmpeg.exe";
            //        //string cmd_args = string.Format(@"-y -t 60 -i {0} {1}test1.mp3", station.Url, station.LocalPath);
            //        //ProcessStartInfo procInfo = new ProcessStartInfo(path, cmd_args);
            //        ////procInfo.CreateNoWindow = false;
            //        //procInfo.UseShellExecute = true;
            //        ////procInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //        //Process procRun = Process.Start(procInfo);
            //        //procRun.WaitForExit();
            //        Console.WriteLine("Done.");
            //    }
            //});
            #endregion

            #region taskProcessing
            UnitOfWork unitOfWork = new UnitOfWork();
            var streamStations = unitOfWork.StreamStationRepository.Get();
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
                            Task<bool> task = capture.StartCapture2(cts.Token,0, station);
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
                unitOfWork.DisposeDbContext();
                streamStations = unitOfWork.StreamStationRepository.Get();
            }
            #endregion

            ////instance processing
            //int stationId = Convert.ToInt32(args[0]);
            //Capture capture = new Capture();
            //var station = db.StreamStations.SingleOrDefault(e => e.Id == stationId);
            //capture.StartCapture(5, station.Url, station.LocalPath);

            Console.WriteLine("Done!!!");
            Console.ReadLine();
        }
        
    }
}
