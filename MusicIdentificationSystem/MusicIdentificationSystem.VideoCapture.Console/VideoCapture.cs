using MusicIdentification.Core;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MusicIdentificationSystem.VideoCapture.Console
{
    public class VideoCapture
    {
        public void Capture()
        {
            // get list of video stations
            // for each station get folder 
            // for each stream folder create a date folder and extract audio there

            var files = Directory.GetFiles(@"C:\Stream\_VIDEO");
            var extractedFile = string.Empty;
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (!IsFileinUse(fileInfo))
                {
                    ExtractAndSaveAudio(fileInfo);
                }
            }

            //StreamStationVideoRepository streamStationRepository = new StreamStationVideoRepository();
            ////UnitOfWork2 unitOfWork = new UnitOfWork2();
            //var streamStations = streamStationRepository.Get();
            //foreach (var streamStation in streamStations)
            //{
            //    if (streamStation.IsActive)
            //    {

            //    }
            //}
        }

        private void SaveStreamFile(string extractedFile)
        {
            StreamVideoRepository streamVideoRepository = new StreamVideoRepository();
            StreamVideoEntity entity = new StreamVideoEntity();

            streamVideoRepository.Insert(entity);
            //throw new NotImplementedException();
        }

        private void ExtractAndSaveAudio(FileInfo fileInfo)
        {
            
            StreamStationVideoEntity streamStation = GetStreamStationVideo(fileInfo.Name);
            // extract audio from video into specific folder
            if (streamStation != null)
            {
                //string newFilePath = fileInfo.FullName.Replace(" ", "_");
                var destinationFolder = $"{cApp.AppSettings["StreamPath"]}{streamStation.LocalPath.Replace(" ", "_")}";
                string newFileName = fileInfo.Name.Replace(" ", "_");
                var newFilePath = $"{destinationFolder}{newFileName}";
                DateTime startDateTime = GetDateFromName(fileInfo.Name, streamStation.StationName);
                File.Copy(fileInfo.FullName, newFilePath, true);
                var destinationFilePath = $"{cApp.AppSettings["StreamPath"]}{streamStation.LocalPath.Replace(" ", "_")}{newFileName.Replace(fileInfo.Extension, ".mp3")}";
                var destinationFile = $"{streamStation.LocalPath.Replace(" ","_")}{newFileName.Replace(fileInfo.Extension,".mp3")}";
                AudioDecoder audioDecoder = new AudioDecoder();
                
                var audioFile = audioDecoder.ExtractAudioFromVideo(newFilePath, destinationFilePath);
                // and save to StreamVideo
                StreamVideoRepository streamVideoRepository = new StreamVideoRepository();
                StreamVideoEntity entity = new StreamVideoEntity();
                entity.FileName = destinationFile;
                entity.StreamStationVideoId = streamStation.Id;
                entity.StartTime = startDateTime;
                entity.EndTime = startDateTime.AddHours(1);
                streamVideoRepository.Insert(entity);
                streamVideoRepository.Save();


            }
          
        }

        private DateTime GetDateFromName(string fileName, string stationName)
        {
            //Music Channel Ro_2018_0506_1955_00

            return new DateTime(
                Convert.ToInt32(fileName.Replace(stationName, "").Replace("_", "").Substring(0, 4)),
                Convert.ToInt32(fileName.Replace(stationName, "").Replace("_", "").Substring(4, 2)),
                Convert.ToInt32(fileName.Replace(stationName, "").Replace("_", "").Substring(6, 2)),
                Convert.ToInt32(fileName.Replace(stationName, "").Replace("_", "").Substring(8, 2)),
                Convert.ToInt32(fileName.Replace(stationName, "").Replace("_", "").Substring(10, 2)),
                Convert.ToInt32(fileName.Replace(stationName, "").Replace("_", "").Substring(12, 2)));
        }
        private static StreamStationVideoEntity GetStreamStationVideo(string file)
        {
            //get the Station Id by station name
            StreamStationVideoRepository streamStationRepository = new StreamStationVideoRepository();
            var streamStations = streamStationRepository.Get();
            foreach (var streamStation in streamStations)
            {
                if (file.Contains(streamStation.StationName))
                    return streamStation;
            }

            return null;
        }

        protected bool IsFileinUse(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
    }
}
