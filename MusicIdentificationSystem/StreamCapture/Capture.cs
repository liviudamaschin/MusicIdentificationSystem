using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StreamCapture
{
    public class Capture
    {
        String serverPath = "/";

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
            TagLib.File f = null ;
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
                    if (Path.GetExtension(srcFileName).ToLower() == ".mp3" )
                    {
                        using (Stream srcStream = File.OpenRead(srcFileName))
                        {
                            srcStream.CopyTo(destStream);
                        }
                    }
                }
            }
        }
        public void StartCapture2(int minutes, string server, string destinationPath)
        {
            HttpWebRequest req;
            Stream s = null;
            Stream fs = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(server);


                WebResponse resp = req.GetResponse();
                s = resp.GetResponseStream();
                var curentHour = DateTime.Now.Hour;
                fs = createNewFile(destinationPath, "defaultStream");


                byte[] buffer = new byte[4096];
                var total = 0;
                var count = 0;
                DateTime startTime = DateTime.Now;
                DateTime endTime = DateTime.Now.AddMinutes(minutes);

                //while (s.CanRead)
                while (minutes == 0 || endTime > DateTime.Now)
                {
                    if (curentHour != DateTime.Now.Hour)
                    {
                        if (fs != null)
                        {
                            fs.Flush();
                            fs.Close();
                        }
                        fs = createNewFile(destinationPath, "defaultStream");
                        curentHour = DateTime.Now.Hour;
                    }
                    int bytesRead = s.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, bytesRead);
                    if (count % 100 == 0)
                        fs.Flush();
                    count++;
                    //total += bytesRead;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (s != null)
                    s.Close();
            }
        }
        /// <summary>
		/// Create new file without overwritin existing files with the same filename.
		/// </summary>
		/// <param name="destPath">destination path of the new file</param>
		/// <param name="filename">filename of the file to be created</param>
		/// <returns>an output stream on the file</returns>
		private static Stream createNewFile(String destPath, String filename)
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
                    return File.Create(filepath);
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
                            return File.Create(filepath);
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
    }
}
