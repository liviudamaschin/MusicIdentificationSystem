using MusicIdentification.Core;
using MusicIdentificationSystem.EF.Entities;
using NAudio.Wave;
using SoundFingerprinting;
using SoundFingerprinting.Audio;
using SoundFingerprinting.Audio.Bass;
using SoundFingerprinting.Audio.NAudio;
using SoundFingerprinting.Builder;
using SoundFingerprinting.DAO.Data;
using SoundFingerprinting.InMemory;
using SoundFingerprinting.Query;
using SoundFingerprinting.SQL;
using StreamCapture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace CreateFingerprint
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        //private readonly IModelService modelService = new InMemoryModelService(); // store fingerprints in RAM
        private readonly IModelService modelService = new SqlModelService(); // store fingerprints in SQL Server Database
        private readonly IAudioService audioService = new NAudioService(); // use NAudio audio processing library

        //private readonly IModelService modelService = new BassModelService(); // store fingerprints in SQL Server Database
        //private readonly IAudioService audioService = new BassAudioService();

        private readonly IFingerprintCommandBuilder fingerprintCommandBuilder = new FingerprintCommandBuilder();
        private readonly IQueryCommandBuilder queryCommandBuilder = new QueryCommandBuilder();
        public BindingList<ListItem> TrackList;

        

        //private readonly IModelService modelService2 = new SqlModelService()
        //public File OpenedFile;
        public MainForm()
        {
            InitializeComponent();

            TrackList = new BindingList<ListItem>();

            bindingSourceTracks.DataSource = TrackList;
        }

        #region Events
        private void btnCreateFingerprint_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
               // pictureBoxWait.Visible = true;

                StoreAudioFileFingerprintsInStorageForLaterRetrieval(openFileDialog.FileName);

                //pictureBoxWait.Visible = false;
            }
        }

        private void btnMatchFingerprint_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                //pictureBoxWait.Visible = true;

                GetBestMatchForSong(openFileDialog.FileName);

                //pictureBoxWait.Visible = false;
            }
        }
        #endregion

        #region Utils
        public void StoreAudioFileFingerprintsInStorageForLaterRetrieval(string pathToAudioFile)
        {
            try
            {
                //TrackList.Clear();
                TagLib.File file = TagLib.File.Create(pathToAudioFile);
                String title = file.Tag.Title;
                String artist = file.Tag.FirstAlbumArtist;
                String album = file.Tag.Album;
                int year = (int)file.Tag.Year;
                double length = file.Properties.Duration.TotalSeconds;
                string isrc = Guid.NewGuid().ToString();

                TrackData track = new TrackData(isrc, artist, title, album, year, length);

                // store track metadata in the datasource
                //var trackReference = modelService.InsertTrack(track);
                var trackReference = modelService.InsertTrack(track);
                // create hashed fingerprints
                var hashedFingerprints = fingerprintCommandBuilder
                                            .BuildFingerprintCommand()
                                            .From(pathToAudioFile)
                                            .UsingServices(audioService)
                                            .Hash()
                                            .Result;

                // store hashes in the database for later retrieval
                modelService.InsertHashDataForTrack(hashedFingerprints, trackReference);

                TrackList.Add(new ListItem(isrc, string.Format("{0} - {1} - {2}", isrc, artist, title)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public TrackData GetBestMatchForSong(string queryAudioFile)
        {
            try
            {
                //TagLib.File file = TagLib.File.Create(queryAudioFile);
                //double length = file.Properties.Duration.TotalSeconds;

                //int secondsToAnalyze = (int)Math.Truncate(length) - 1; // number of seconds to analyze from query file



                ////////////////////read all headers
                //List<string> arrHeaders = new List<string>();
                //List<Tuple<int, string, string>> attributes = new List<Tuple<int, string, string>>();

                //Shell32.Shell shell = new Shell32.Shell();
                //var strFileName = queryAudioFile;
                //Shell32.Folder objFolder = shell.NameSpace(System.IO.Path.GetDirectoryName(strFileName));
                //Shell32.FolderItem folderItem = objFolder.ParseName(System.IO.Path.GetFileName(strFileName));


                //for (int i = 0; i < short.MaxValue; i++)
                //{
                //    string header = objFolder.GetDetailsOf(null, i);
                //    if (String.IsNullOrEmpty(header))
                //        break;
                //    arrHeaders.Add(header);
                //}

                //for (int i = 0; i < arrHeaders.Count; i++)
                //{
                //    var attrName = arrHeaders[i];
                //    var attrValue = objFolder.GetDetailsOf(folderItem, i);
                //    var attrIdx = i;

                //    attributes.Add(new Tuple<int, string, string>(attrIdx, attrName, attrValue));

                //    Debug.WriteLine("{0}\t{1}: {2}", i, attrName, attrValue);
                //}

                /////////////////end

                //WindowsMediaPlayer wmp = new WindowsMediaPlayer();
                //IWMPMedia song = wmp.newMedia(queryAudioFile);

                FileInfo fi = new FileInfo(queryAudioFile);

                //int secondsToAnalyze = (int)fi.Length/6000;
                //int secondsToAnalyze = 120;
                int startAtSecond = 0; // start at the begining

                Mp3FileReader reader = new Mp3FileReader(queryAudioFile);
                //WaveFileReader reader = new WaveFileReader(queryAudioFile);
                int secondsToAnalyze = (int)Math.Truncate(reader.TotalTime.TotalSeconds)-1;
                // query the underlying database for similar audio sub-fingerprints

                Stopwatch sw = new Stopwatch();
                sw.Start();
                //TrackList.Clear();
                var queryResult = queryCommandBuilder.BuildQueryCommand()
                                                     .From(queryAudioFile, secondsToAnalyze, startAtSecond)
                                                     .UsingServices(modelService, audioService)
                                                     .Query()
                                                     .Result;
                sw.Stop();
                MessageBox.Show(String.Format("Time elapsed {0}:{1}", sw.Elapsed.TotalMinutes, sw.Elapsed.TotalSeconds));
                foreach (ResultEntry result in queryResult.ResultEntries)
                {
                    TrackList.Add(new ListItem(result.Track.ISRC, string.Format("{0} - {1} : {2}", result.Track.Artist, result.Track.Title, result.TrackMatchStartsAt)));
                }
                // MessageBox.Show(string.Format("{0} - {1} - {2}", queryResult.BestMatch.Track.ISRC, queryResult.BestMatch.Track.Artist, queryResult.BestMatch.Track.Title));


                if (queryResult.BestMatch != null && queryResult.BestMatch.Track != null)
                    return queryResult.BestMatch.Track; // successful match has been found
                else
                    return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Error.ManageError(ex));
                return null;
            }
        }

        private void DeleteFiles(string folderPath, string fileExtension)
        {
            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.ToLower() == fileExtension)
                {
                    File.Delete(fileInfo.FullName);
                }

            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox1.Text))
                return;
            if (!Directory.Exists(textBox2.Text))
                Directory.CreateDirectory(textBox2.Text);
            AudioDecoder audioDecoder = new AudioDecoder();
            //audioDecoder.ConvertMp3ToWavFolder(textBox1.Text, textBox2.Text);
            //audioDecoder.ConvertWavToMp3Folder(textBox2.Text, textBox2.Text);
            //DeleteFiles(textBox2.Text, ".mp3");
            audioDecoder.NormalizeMp3(textBox1.Text, textBox2.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath + @"\";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath + @"\"; ;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sourcePath = textBox3.Text;


            TrackList.Clear();
            Fingerprint fingerprint = new Fingerprint();
            List<ResultEntity> traks = fingerprint.GetMatchSongsFromFolder(textBox3.Text);
            foreach (var track in traks)
            {
                TrackList.Add(new ListItem(track.Track.Isrc, string.Format("{0} - {1}", track.Track.Artist, track.Track.Title)));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath + @"\"; ;
            }
        }
    }
}
