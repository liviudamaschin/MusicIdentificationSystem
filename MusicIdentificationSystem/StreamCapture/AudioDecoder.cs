using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCapture
{
    public class AudioDecoder
    {
        private void ConvertMp3ToWav(string source, string destination)
        {
            //Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ffmpeg.exe";
            startInfo.Arguments = String.Format(@"-i {0} -acodec libmp3lame {1}", source, destination);
            //.StartInfo.Arguments = "-threads 2"
            //process.StartInfo = startInfo;
            //process.Start();
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // Log error.
            }
        }

        private void ConvertWavToMp3(string source, string destination)
        {
            //Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ffmpeg.exe";
            startInfo.Arguments = String.Format(@"-i {0} -acodec libmp3lame {1}", source, destination);
            //process.StartInfo = startInfo;
            //process.Start();
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // Log error.
            }
        }

        public void ConvertMp3ToWavFolder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            Parallel.ForEach(files, file =>
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.ToLower() == ".mp3")
                {
                    ConvertMp3ToWav(fileInfo.FullName, String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")));
                }
            });
           
        }

        public void ConvertWavToMp3Folder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            Parallel.ForEach(files, file =>
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.ToLower() == ".mp3")
                {
                    ConvertWavToMp3(fileInfo.FullName, String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".mp3")));
                }
            });
            
        }

        public void NormalizeMp3Folder(string sourceFolder, string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            Parallel.ForEach(files, file =>
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.ToLower() == ".mp3")
                {
                    ConvertMp3ToWav(fileInfo.FullName, String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")));
                    ConvertWavToMp3(String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")), String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".mp3")));
                    File.Delete(String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")));
                }
            });
        }

        public string NormalizeMp3(string source, string destinationFolder)
        {
            
            FileInfo fileInfo = new FileInfo(source);
            string destinationFile = String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".mp3"));
            ConvertMp3ToWav(fileInfo.FullName, String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")));
            ConvertWavToMp3(String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")), destinationFile);
            File.Delete(String.Format("{0}{1}", destinationFolder, fileInfo.Name.Replace(fileInfo.Extension, ".wav")));

            return destinationFile;
        }
    }
}
