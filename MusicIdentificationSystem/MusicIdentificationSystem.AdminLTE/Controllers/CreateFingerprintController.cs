using MusicIdentification.Core;
using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Models.CreateFingerprint;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MusicIdentificationSystem.AdminLTE.Controllers
{
    public class CreateFingerprintController : BaseController
    {
        private AccountRepository accountRepository = new AccountRepository();
        private TrackRepository trackRepository = new TrackRepository();
        private AccountXTrackRepository accountXTrackRepository = new AccountXTrackRepository();
        private ApplicationSettingRepository applicationSettingRepository = new ApplicationSettingRepository();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("CreateFingerprintList");
        }

        [HttpGet]
        public ActionResult CreateFingerprintList(int accountId)
        {
            var account = accountRepository.GetByID(accountId);

            ViewBag.AccountName = account != null ? account.AccountName : null;

            var createFingerprintListModel = new FingerprintListModel();
            createFingerprintListModel.AccountId = accountId;

            var tracksList = trackRepository.Get();
            var accountXTracksList = accountXTrackRepository.Get(x => x.AccountId == accountId);

            createFingerprintListModel.FingerprintModelsList = (from t in tracksList
                                                                join at in accountXTracksList
                                                                on t.Id equals at.TrackId
                                                                select t)
                                                                .OrderByDescending(x => x.IsNew)
                                                                .OrderByDescending(x => x.IsActive)
                                                                .Select(x => x.ToModel())
                                                                .ToList();

            return View("CreateFingerprintList", createFingerprintListModel);
        }

        #endregion List

        #region CreateFingerprint

        [HttpPost]
        public async Task<JsonResult> CreateFingerprint(int accountId)
        {
            JsonResult result = null;

            try
            {
                var folderPathSetting = applicationSettingRepository.Get(x => (x.AccountId == null || x.AccountId == accountId)
                                                                        && x.IsActive == true
                                                                        && string.Equals(x.KeyName, Constants.AdminLTE_CreateFingerprint_UploadFolder_KeyName))
                                                                .OrderByDescending(x => x.AccountId)
                                                                .FirstOrDefault();

                string folderPath = folderPathSetting != null ? folderPathSetting.KeyValue : Constants.AdminLTE_CreateFingerprint_UploadFolder_KeyValue;

                #region Save Files on Server

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = Path.GetFileName(fileContent.FileName);
                        var path = Path.Combine(folderPath, fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }

                #endregion

                #region Clear IsNew from Account Tracks

                trackRepository.ClearIsNewFromAccountTracks(accountId);

                #endregion

                #region Create Fingerprints

                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.Extension.ToLower() == ".mp3")
                    {
                        Fingerprint fingerprint = new Fingerprint();
                        fingerprint.StoreAudioFileFingerprintsInStorageForLaterRetrieval(file, accountId);
                    }

                }

                #endregion

                #region Delete Uploaded Files

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.Delete();
                }

                #endregion

                result = Json(Resources.Resources.CreateFingerprint_Success);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                result = Json(Resources.Resources.CreateFingerprint_Error + System.Environment.NewLine + ex.Message);
            }

            return result;
        }

        #endregion
    }
}