using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.Track;
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
    public class TrackController : BaseController
    {
        private TrackRepository trackRepository = new TrackRepository();
        private AccountTrackRepository accountTrackRepository = new AccountTrackRepository();
        private AccountRepository accountRepository = new AccountRepository();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("TrackList");
        }

        [HttpGet]
        public ActionResult TrackList()
        {
            var trackListModel = new TrackListModel();

            var tracksList = trackRepository.Get();

            trackListModel.TrackModelsList = tracksList.Select(x => x.ToModel()).ToList();

            return View("TrackList", trackListModel);
        }

        [HttpGet]
        public ActionResult AccountTrackList(int accountId)
        {
            var account = accountRepository.GetByID(accountId);

            ViewBag.AccountName = account != null ? account.AccountName : null;

            var trackListModel = new TrackListModel();

            var tracksList = trackRepository.Get();
            var accountTracksList = accountTrackRepository.Get(x => x.AccountId == accountId);

            trackListModel.TrackModelsList = (from t in tracksList
                                              join at in accountTracksList
                                              on t.Id equals at.TrackId
                                              select t).Select(x => x.ToModel()).ToList();

            return View("TrackList", trackListModel);
        }
        #endregion List

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var model = new TrackModel();
            model.IsActive = true;
            return View("CreateTrack", model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(TrackModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Length = Utils.GetTotalSecondsFromText(model.LengthText);

                    var Track = model.ToEntity();

                    trackRepository.Insert(Track);
                    trackRepository.Save();

                    SuccessNotification(Resources.Resources.Track_SuccessfullyCreated);

                    return continueEditing ? RedirectToAction("Edit", new { id = Track.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("CreateTrack", model);
                }
            }
            else
            {
                return View("CreateTrack", model);
            }
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Track = trackRepository.GetByID(id);

            if (Track != null)
            {
                var model = Track.ToModel();

                if (model.Length.HasValue)
                {
                    var timespan = TimeSpan.FromSeconds(model.Length.Value);
                    //here backslash is must to tell that colon is
                    //not the part of format, it just a character that we want in output
                    model.LengthText = timespan.ToString(@"hh\:mm\:ss\:fff");
                }

                return View("EditTrack", model);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(TrackModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Length = Utils.GetTotalSecondsFromText(model.LengthText);

                    var track = model.ToEntity();

                    trackRepository.Update(track);
                    trackRepository.Save();

                    SuccessNotification(Resources.Resources.Track_SuccessfullyUpdated);
                    return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("EditTrack", model);
                }
            }
            else
            {
                return View("EditTrack", model);
            }
        }

        #endregion
    }
}