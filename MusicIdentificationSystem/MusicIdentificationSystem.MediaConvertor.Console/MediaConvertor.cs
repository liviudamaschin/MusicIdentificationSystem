using MusicIdentification.Core;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MusicIdentificationSystem.MediaConvertor
{
    public class MediaConvertor
    {
        protected static readonly int Maxthreads = 2;

        protected static int BatchActiveThreadsCount = 0;
        

        public static void ConvertFiles()
        {
            //UnitOfWork2 unitOfWork = new UnitOfWork2();
            StreamRepository streamRepository = new StreamRepository();
            StreamStationRepository streamStationRepository = new StreamStationRepository();
            var streams = streamRepository.GetUnconvertedStreams();
            foreach (var stream in streams)
            {
                string convertedFileName;
                string fileName = Path.Combine(cApp.AppSettings["StreamPath"], stream.FileName);
                var streamStation = streamStationRepository.GetByID(stream.StationId);
                Console.WriteLine($"Converting file {fileName}");
                convertedFileName = MediaConvertor.ConvertCurrentFile(stream.Id, fileName, streamStation.LocalPath, streamStation.TransformFolder);
               
                Console.WriteLine($"converted file = {convertedFileName}");
            }

        }
        private static string ConvertCurrentFile(int streamId, string sourceFile, string folder, string convertFolder)
        {
            StreamRepository streamRepository = new StreamRepository();
            string retFile = string.Empty;
            //while (MediaConvertor.BatchActiveThreadsCount >= MediaConvertor.Maxthreads)
            //{
            //    Thread.Sleep(100);
            //}
            string fullFilePath = Path.Combine(cApp.AppSettings["StreamPath"], sourceFile);
            AudioDecoder decoder = new AudioDecoder();
            FileInfo fi = new FileInfo(fullFilePath);
            //string dateFolder = fi.FullName.Replace(folder, "").Replace(fi.Name, "");
            string destinationFolder = fi.FullName.Replace(fi.Name, "");
            destinationFolder = Path.Combine(destinationFolder, convertFolder);
            // create directory, if it doesn't exist
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);
            try
            {
                //MediaConvertor.BatchActiveThreadsCount++;

                retFile = decoder.NormalizeMp3(fullFilePath, destinationFolder);
                retFile = retFile.Replace(cApp.AppSettings["StreamPath"], "");
                var convertedStream = streamRepository.GetByID(streamId);
                convertedStream.FileNameTransformed = retFile;
                streamRepository.Save();
                  //  MediaConvertor.BatchActiveThreadsCount--;
                
            }
            catch (Exception)
            {

                throw;
            }
            return retFile;
        }
    }
}
