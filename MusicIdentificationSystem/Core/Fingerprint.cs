using System;
using SoundFingerprinting;
using SoundFingerprinting.Audio;
using SoundFingerprinting.Audio.NAudio;
using SoundFingerprinting.Builder;
using SoundFingerprinting.DAO.Data;
using SoundFingerprinting.Query;
using SoundFingerprinting.SQL;
using System.IO;
using NAudio.Wave;
using System.Collections.Generic;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.DatabaseConfiguration;
using MusicIdentificationSystem.DAL.UnitOfWork;
using MusicIdentificationSystem.DAL.Repositories;
using System.Linq;

namespace MusicIdentification.Core
{
    public class Fingerprint
    {
        //private readonly IModelService modelService = new InMemoryModelService(); // store fingerprints in RAM
        private readonly IModelService modelService = new SqlModelService(); // store fingerprints in SQL Server Database
        private readonly IAudioService audioService = new NAudioService(); // use NAudio audio processing library
        private readonly IFingerprintCommandBuilder fingerprintCommandBuilder = new FingerprintCommandBuilder();
        private readonly IQueryCommandBuilder queryCommandBuilder = new QueryCommandBuilder();
        //private UnitOfWork2 unitOfWork;
        private IDatabaseConfigurationManager config;
        private readonly AccountXTrackRepository accountXTrackRepository = new AccountXTrackRepository();
        private readonly TrackRepository trackRepository = new TrackRepository();

        public Fingerprint()
        {
            this.config = config;
            //this.unitOfWork = new UnitOfWork2();
        }

        public void StoreAudioFileFingerprintsInStorageForLaterRetrieval(string pathToAudioFile, int? accountId = null)
        {
            try
            {
                TagLib.File file = TagLib.File.Create(pathToAudioFile);
                String title = file.Tag.Title;
                String artist = file.Tag.FirstAlbumArtist;
                String album = file.Tag.Album;
                int year = (int)file.Tag.Year;
                double length = file.Properties.Duration.TotalSeconds;
                string isrc = Guid.NewGuid().ToString();

                TrackData track = new TrackData(isrc, artist, title, album, year, length);

                // store track metadata in the datasource
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

                // create link between track and account
                var trackEntity = trackRepository.Get(x => x.Isrc == isrc).FirstOrDefault();

                if (trackEntity != null && accountId.HasValue)
                {
                    var accountXTrackEntity = new AccountXTrackEntity()
                    {
                        AccountId = accountId.Value,
                        TrackId = trackEntity.Id,
                        IsActive = true
                    };

                    accountXTrackRepository.Insert(accountXTrackEntity);
                    accountXTrackRepository.Save();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message);
            }
        }

        public List<ResultEntity> GetBestMatchForSong(string queryAudioFile, double confidence)
        {
            List<ResultEntity> tracks = new List<ResultEntity>();
            if (!File.Exists(queryAudioFile))
                return tracks;
            try
            {

                int startAtSecond = 0; // start at the begining

                Mp3FileReader reader = new Mp3FileReader(queryAudioFile);
                //WaveFileReader reader = new WaveFileReader(queryAudioFile);
                int secondsToAnalyze = (int)Math.Truncate(reader.TotalTime.TotalSeconds) - 1;
                // query the underlying database for similar audio sub-fingerprints

                var queryResult = queryCommandBuilder.BuildQueryCommand()
                                                     .From(queryAudioFile, secondsToAnalyze, startAtSecond)
                                                     .UsingServices(modelService, audioService)
                                                     .Query()
                                                     .Result;
                
                foreach (ResultEntry result in queryResult.ResultEntries)
                {
                    
                    if (result.Confidence > confidence)
                    {
                        ResultEntity trackresult = new ResultEntity();
                        trackresult.Filename = queryAudioFile;
                        trackresult.TrackId = Convert.ToInt32(result.Track.TrackReference.Id);
                        trackresult.TrackMatchStartAt = Convert.ToDecimal(result.TrackMatchStartsAt);
                        trackresult.QueryMatchStartsAt = Convert.ToDecimal(result.QueryMatchStartsAt);
                        trackresult.QueryMatchLength = Convert.ToDecimal(result.QueryMatchLength);
                        trackresult.TrackStartsAt = Convert.ToDecimal(result.TrackStartsAt);
                        trackresult.Coverage = Convert.ToDecimal(result.Coverage);
                        trackresult.Confidence = Convert.ToDecimal(result.Confidence);

                        //trackresult.Track = unitOfWork.TrackRepository.GetByID(trackresult.TrackId);
                        tracks.Add(trackresult);
                    }

                }

                return tracks;

                //if (queryResult.BestMatch != null && queryResult.BestMatch.Track != null)
                //    return queryResult.BestMatch.Track; // successful match has been found
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(Error.ManageError(ex));
                return null;
            }
        }

        public List<ResultEntity> GetMatchSongsFromFolder(string sourcePath, double confidence)
        {
            List<ResultEntity> tracks = new List<ResultEntity>();
            if (!Directory.Exists(sourcePath))
                return null;

            string[] files = Directory.GetFiles(sourcePath);
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.ToLower() == ".mp3")
                {
                    List<ResultEntity> matchedTracks = null;
                    matchedTracks = GetBestMatchForSong(fileInfo.FullName, confidence);
                    if (matchedTracks != null && matchedTracks.Count>0)
                        tracks.AddRange(matchedTracks);
                }
            }
            return tracks;
        }
    }
}
