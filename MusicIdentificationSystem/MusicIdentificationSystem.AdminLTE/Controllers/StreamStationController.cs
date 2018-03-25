using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.StreamStation;
using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicIdentificationSystem.AdminLTE.Controllers
{
    public class StreamStationController : BaseController
    {
        private StreamStationRepository streamStationRepository = new StreamStationRepository();
        private AccountRepository accountRepository = new AccountRepository();
        private AccountXTrackRepository accountXTrackRepository = new AccountXTrackRepository();
        private StreamStationXTrackRepository streamStationXTrackRepository = new StreamStationXTrackRepository();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("StreamStationList");
        }

        [HttpGet]
        public ActionResult StreamStationList()
        {
            var streamStationListModel = new StreamStationListModel();

            var streamStationsList = streamStationRepository.Get();

            streamStationListModel.StreamStationModelsList = streamStationsList.Select(x => x.ToModel()).ToList();

            return View("StreamStationList", streamStationListModel);
        }

        [HttpGet]
        public ActionResult AccountXStreamStationList(int accountId)
        {
            var account = accountRepository.GetByID(accountId);

            ViewBag.AccountName = account != null ? account.AccountName : null;

            var streamStationListModel = new StreamStationListModel();

            var streamStationsList = streamStationRepository.Get(x => x.IsActive == true);

            var accountXTracksList = accountXTrackRepository.Get(x => x.AccountId == accountId);

            var streamStationXTracksList = streamStationXTrackRepository.Get(x => x.IsActive == true);

            streamStationListModel.StreamStationModelsList = (from at in accountXTracksList
                                                              join st in streamStationXTracksList
                                                              on at.TrackId equals st.TrackId
                                                              join s in streamStationsList
                                                              on st.StreamStationId equals s.Id
                                                              select s).Select(x => x.ToModel()).ToList();

            return View("StreamStationList", streamStationListModel);
        }
        #endregion List

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var model = new StreamStationModel();
            model.IsActive = true;
            return View("CreateStreamStation", model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(StreamStationModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var StreamStation = model.ToEntity();

                    streamStationRepository.Insert(StreamStation);
                    streamStationRepository.Save();

                    SuccessNotification(Resources.Resources.StreamStation_SuccessfullyCreated);

                    return continueEditing ? RedirectToAction("Edit", new { id = StreamStation.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("CreateStreamStation", model);
                }
            }
            else
            {
                return View("CreateStreamStation", model);
            }
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var streamStation = streamStationRepository.GetByID(id);

            if (streamStation != null)
            {
                var model = streamStation.ToModel();

                return View("EditStreamStation", model);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(StreamStationModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var setting = model.ToEntity();

                    streamStationRepository.Update(setting);
                    streamStationRepository.Save();

                    SuccessNotification(Resources.Resources.StreamStation_SuccessfullyUpdated);
                    return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("EditStreamStation", model);
                }
            }
            else
            {
                return View("EditStreamStation", model);
            }
        }

        #endregion
    }
}