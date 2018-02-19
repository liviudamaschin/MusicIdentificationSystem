
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentification.Core;
using MusicIdentificationSystem.Common;

namespace MusicIdentificationSystem.StreamCapture
{
    public class Capture
    {
        String serverPath = "/";

        //private UnitOfWork2 unitOfWork;
        StreamRepository streamRepository = new StreamRepository();
        private HttpWebRequest req;
        private Stream stream = null;
        private FileStream fs = null;
        private StreamEntity streamEntity = new StreamEntity();

        private AudioDecoder audioDecoder = new AudioDecoder();
        public Capture()
        {
            //this.unitOfWork = new UnitOfWork2();
        }

        public void StartCapture(int minutes, string server, string destinationPath)
        {
            HttpWebRequest request = null; // web request
            HttpWebResponse response = null; // web response

            int metaInt = 0; // blocksize of mp3 data
            int count = 0; // byte counter
            int metadataLength = 0; // length of metadata header

            byte[] buffer = new byte[512]; // receive buffer

            Stream socketStream = null; // input stream on the web request
            Stream byteOut = null; // output stream on the destination file
            TagLib.File f = null;
            // create web request
            request = (HttpWebRequest)WebRequest.Create(server);

            // clear old request header and build own header to receive ICY-metadata
            request.Headers.Clear();
            request.Headers.Add("GET", serverPath + " HTTP/1.0");
            request.Headers.Add("Icy-MetaData", "1"); // needed to receive metadata informations
            request.UserAgent = "WinampMPEG/5.09";

            // execute request
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            // read blocksize to find metadata header
            metaInt = Convert.ToInt32(response.GetResponseHeader("icy-metaint"));
            var curentHour = DateTime.Now.Hour;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddMinutes(minutes);

            try
            {
                // open stream on response
                socketStream = response.GetResponseStream();
                byteOut = createNewFile(destinationPath, "defaultStream");
                f = TagLib.File.Create(((System.IO.FileStream)byteOut).Name);
                // rip stream in an endless loop
                //while (byteOut.Length < 1024000) // 23650000 ~ 30 min     
                while (minutes == 0 || endTime > DateTime.Now)
                {
                    if (curentHour != DateTime.Now.Hour)
                    {
                        if (byteOut != null)
                        {
                            byteOut.Flush();
                            byteOut.Close();
                            f.Save();
                        }
                        byteOut = createNewFile(destinationPath, "defaultStream");
                        f = TagLib.File.Create(((System.IO.FileStream)byteOut).Name);
                        curentHour = DateTime.Now.Hour;
                    }
                    // read byteblock
                    int bufLen = socketStream.Read(buffer, 0, buffer.Length);
                    if (bufLen < 0)
                        return;
                    if (byteOut != null) // as long as we don't have a songtitle, we don't open a new file and don't write any bytes
                    {
                        byteOut.Write(buffer, 0, bufLen);
                        //byteOut.Flush();
                    }

                    //for (int i = 0; i < bufLen; i++)
                    //{
                    //    if (count++ < metaInt) // write bytes to filestream
                    //    {
                    //        if (byteOut != null) // as long as we don't have a songtitle, we don't open a new file and don't write any bytes
                    //        {
                    //            byteOut.Write(buffer, i, 1);
                    //            if (count % 100 == 0)
                    //                byteOut.Flush();
                    //        }
                    //    }
                    //    else // get headerlength from lengthbyte and multiply by 16 to get correct headerlength
                    //    {
                    //        metadataLength = Convert.ToInt32(buffer[i]) * 16;
                    //        count = 0;
                    //    }
                    //}
                }
            }
            //try
            //{
            //    // open stream on response
            //    socketStream = response.GetResponseStream();
            //    byteOut = createNewFile(destinationPath, "defaultStream");
            //    // rip stream in an endless loop
            //    while (endTime > DateTime.Now)
            //    {
            //        // read byteblock
            //        int bufLen = socketStream.Read(buffer, 0, buffer.Length);
            //        if (bufLen < 0)
            //            return;

            //        for (int i = 0; i < bufLen; i++)
            //        {

            //            if (byteOut != null) // as long as we don't have a songtitle, we don't open a new file and don't write any bytes
            //            {
            //                byteOut.Write(buffer, i, 1);
            //                //if (count % 100 == 0)
            //                    byteOut.Flush();
            //            }

            //        }
            //    }
            //}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (byteOut != null)
                {
                    byteOut.Close();
                    if (f != null)
                        f.Save();
                }
                if (socketStream != null)
                    socketStream.Close();
            }
        }

        //internal Task<bool> StartCapture2(CancellationToken token, int v, StreamStationEntity station)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderpath"></param>
        public void combineFolderContent(string folderpath)
        {
            //string[] srcFileNames = { "file1.txt", "file2.txt", "file3.txt" };
            var destinationPath = Path.Combine(folderpath, "out");
            string destFileName = String.Format("{0}\\stream.mp3", destinationPath);

            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);

            using (Stream destStream = File.OpenWrite(destFileName))
            {
                foreach (string srcFileName in Directory.GetFiles(folderpath))
                {
                    if (Path.GetExtension(srcFileName).ToLower() == ".mp3")
                    {
                        using (Stream srcStream = File.OpenRead(srcFileName))
                        {
                            srcStream.CopyTo(destStream);
                        }
                    }
                }
            }
        }

        internal async Task<bool> StartCapture2(CancellationToken ct,int minutes, StreamStationEntity station)
        {
            await Task.Run(() => {


                Console.WriteLine("Start capture station {0}", station.StationName);
                //StreamEntity streamEntity = null;
                try
                {
                    //station.Url, station.LocalPath
                    req = (HttpWebRequest)WebRequest.Create(station.Url);

                    WebResponse resp = req.GetResponse();
                    stream = resp.GetResponseStream();
                    var curentHour = DateTime.Now.Hour;
                    
                    fs = createNewFile(Path.Combine(cApp.AppSettings["StreamPath"], station.LocalPath), "defaultStream");
                    //stream = new StreamEntity();

                    streamEntity.StationId = station.Id;
                    streamEntity.FileName = fs.Name.Replace(cApp.AppSettings["StreamPath"],"");
                    streamEntity.StartTime = DateTime.Now;

                    streamRepository.Insert(streamEntity);
                    streamRepository.Save();
                    byte[] buffer = new byte[4096];
                    var total = 0;
                    var count = 0;
                    DateTime startTime = DateTime.Now;
                    DateTime endTime = DateTime.Now.AddMinutes(minutes);

                    //while (s.CanRead)
                    while (minutes == 0 || endTime > DateTime.Now || !ct.IsCancellationRequested)
                    {
                        if (curentHour != DateTime.Now.Hour)
                        {
                            if (fs != null)
                            {
                                fs.Flush();
                                fs.Close();
                            }
                            streamEntity.EndTime = DateTime.Now;
                            //unitOfWork.StreamRepository.Insert(stream);
                            //TODO: convert file
                            //streamEntity.FileNameTransformed = ConvertCurrentFile(station.LocalPath, station.TransformFolder);
                            streamRepository.Save();

                            fs = createNewFile(Path.Combine(cApp.AppSettings["StreamPath"], station.LocalPath), "defaultStream");

                            streamEntity = new StreamEntity();
                            streamEntity.StationId = station.Id;
                            streamEntity.FileName = fs.Name.Replace(cApp.AppSettings["StreamPath"], "");
                            streamEntity.StartTime = DateTime.Now;
                            streamRepository.Insert(streamEntity);
                            streamRepository.Save();
                            curentHour = DateTime.Now.Hour;
                        }
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        fs.Write(buffer, 0, bytesRead);
                        streamEntity.EndTime = DateTime.Now;
                        streamRepository.Save();
                        //if (count % 100 == 0)
                            fs.Flush();
                        count++;
                        //total += bytesRead;
                    }
                    streamEntity.EndTime = DateTime.Now;
                    //unitOfWork.StreamRepository.Insert(stream);
                    //TODO: convert file
                    //streamEntity.FileNameTransformed = ConvertCurrentFile(station.LocalPath, station.TransformFolder);
                    streamRepository.Save();
                    
                    //return true;

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //return false;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                    if (stream != null)
                        stream.Close();
                    if (streamEntity != null)
                    {
                        streamEntity.EndTime = DateTime.Now;
                        //TODO: convert file
                        //streamEntity.FileNameTransformed= ConvertCurrentFile(station.LocalPath, station.TransformFolder);
                        streamRepository.Save();
                        
                    }

                }
            },ct);
            //Console.WriteLine("End capture station {0}", station.StationName);
            return true;
        }

        /// <summary>
		/// Create new file without overwritin existing files with the same filename.
		/// </summary>
		/// <param name="destPath">destination path of the new file</param>
		/// <param name="filename">filename of the file to be created</param>
		/// <returns>an output stream on the file</returns>
		private static FileStream createNewFile(String destPath, String filename)
        {
            // replace characters, that are not allowed in filenames. (quick and dirrrrrty ;) )
            filename = filename.Replace(":", "");
            filename = filename.Replace("/", "");
            filename = filename.Replace("\\", "");
            filename = filename.Replace("<", "");
            filename = filename.Replace(">", "");
            filename = filename.Replace("|", "");
            filename = filename.Replace("?", "");
            filename = filename.Replace("*", "");
            filename = filename.Replace("\"", "");
            filename = String.Format("{0}_{1}", filename, DateTime.Now.Hour);
            try
            {
                var datepath =String.Format("{0}\\",DateTime.Now.ToString("yyyy-MM-dd"));
                destPath = Path.Combine(destPath, datepath);
                // create directory, if it doesn't exist
                if (!Directory.Exists(destPath))
                    Directory.CreateDirectory(destPath);

                // create new file

               

                var filepath = String.Format("{0}.mp3", Path.Combine(destPath + filename));
                if (!File.Exists(filepath))
                {
                    //return File.Create(filepath);
                    return new FileStream(filepath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                    //TagLib.File f = TagLib.File.Create(filepath);
                    //return f;
                    //f.Tag.Album = "New Album Title";
                    //f.Save();
                }
                else // if file already exists, don't overwrite it. Instead, create a new file named <filename>(i).mp3
                {
                    for (int i = 1; ; i++)
                    {
                        filepath = String.Format("{0}({1}).mp3", Path.Combine(destPath + filename),i);
                        if (!File.Exists(filepath))
                        {
                            //return File.Create(filepath);
                            return new FileStream(filepath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
                        }
                    }
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

        public void record()
        {
            // http://relay.pandora.radioabf.net:9000
            String server = "http://radio.mosaiquefm.net:8000/mosalive";
            String serverPath = "/";

            String destPath = "A:\\";           // destination path for saved songs
            String fname = "test";
            HttpWebRequest request = null; // web request
            HttpWebResponse response = null; // web response

            int metaInt = 0; // blocksize of mp3 data
            int count = 0; // byte counter
            int metadataLength = 0; // length of metadata header

            byte[] buffer = new byte[512]; // receive buffer

            Stream socketStream = null; // input stream on the web request
            Stream byteOut = null; // output stream on the destination file

            // create web request
            request = (HttpWebRequest)WebRequest.Create(server);

            // clear old request header and build own header to receive ICY-metadata
            request.Headers.Clear();
            request.Headers.Add("GET", serverPath + " HTTP/1.0");
            request.Headers.Add("Icy-MetaData", "1"); // needed to receive metadata informations
            request.UserAgent = "WinampMPEG/5.09";

            // execute request
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            // read blocksize to find metadata header
            metaInt = Convert.ToInt32(response.GetResponseHeader("icy-metaint"));

            try
            {
                // open stream on response
                socketStream = response.GetResponseStream();
                byteOut = createNewFile(destPath, fname);
                // rip stream in an endless loop
                while (byteOut.Length < 1024000) // 23650000 ~ 30 min     
                {
                    // read byteblock
                    int bufLen = socketStream.Read(buffer, 0, buffer.Length);
                    if (bufLen < 0)
                        return;

                    for (int i = 0; i < bufLen; i++)
                    {
                        if (count++ < metaInt) // write bytes to filestream
                        {
                            if (byteOut != null) // as long as we don't have a songtitle, we don't open a new file and don't write any bytes
                            {
                                byteOut.Write(buffer, i, 1);
                                if (count % 100 == 0)
                                    byteOut.Flush();
                            }
                        }
                        else // get headerlength from lengthbyte and multiply by 16 to get correct headerlength
                        {
                            metadataLength = Convert.ToInt32(buffer[i]) * 16;
                            count = 0;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (byteOut != null)
                    byteOut.Close();
                if (socketStream != null)
                    socketStream.Close();
            }
        }

        private string ConvertCurrentFile(string folder, string convertFolder)
        {
            if (fs == null)
                return null;
            AudioDecoder decoder = new AudioDecoder();
            FileInfo fi = new FileInfo(fs.Name);
            string dateFolder = fs.Name.Replace(folder, "").Replace(fi.Name, "");
            string destinationFolder = Path.Combine(folder, dateFolder);
            destinationFolder = Path.Combine(destinationFolder, convertFolder);
            // create directory, if it doesn't exist
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            return decoder.NormalizeMp3(fs.Name, destinationFolder);
        }

        ~Capture()
        {

            //unitOfWork.Save();
        }
    }
}
