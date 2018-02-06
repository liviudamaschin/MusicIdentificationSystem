using MusicIdentification.Core;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            var streams = streamRepository.GetList(x => x.FileNameTransformed == null);
            foreach (var stream in streams)
            {
                string convertedFileName;
                var streamStation = streamStationRepository.GetByID(stream.StationId);
                Console.WriteLine($"Converting file {stream.FileName}");
                convertedFileName=MediaConvertor.ConvertCurrentFile(stream.FileName, streamStation.LocalPath, streamStation.TransformFolder);
                Console.WriteLine($"converted file = {convertedFileName}");
            }

        }
        private static string ConvertCurrentFile(string sourceFile, string folder, string convertFolder)
        {
            string retFile = string.Empty;
            while (MediaConvertor.BatchActiveThreadsCount >= MediaConvertor.Maxthreads)
            {
                Thread.Sleep(100);
            }
            AudioDecoder decoder = new AudioDecoder();
            FileInfo fi = new FileInfo(sourceFile);
            string dateFolder = fi.FullName.Replace(folder, "").Replace(fi.Name, "");
            string destinationFolder = Path.Combine(folder, dateFolder);
            destinationFolder = Path.Combine(destinationFolder, convertFolder);
            // create directory, if it doesn't exist
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);
            try
            {
                MediaConvertor.BatchActiveThreadsCount++;
                new Task(() =>
                {
                    retFile = decoder.NormalizeMp3(sourceFile, destinationFolder);
                    MediaConvertor.BatchActiveThreadsCount--;
                }).Start();
            }
            catch (Exception)
            {

                throw;
            }
            return retFile;
        }
    }
}
