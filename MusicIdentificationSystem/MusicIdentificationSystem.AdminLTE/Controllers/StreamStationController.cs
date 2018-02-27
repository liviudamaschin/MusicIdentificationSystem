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
        private StreamStationRepository StreamStationRepository = new StreamStationRepository();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("StreamStationList");
        }

        [HttpGet]
        public ActionResult StreamStationList()
        {
            var StreamStationListModel = new StreamStationListModel();

            var StreamStationsList = StreamStationRepository.Get();

            StreamStationListModel.StreamStationModelsList = StreamStationsList.Select(x => x.ToModel()).ToList();

            return View("StreamStationList", StreamStationListModel);
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

                    StreamStationRepository.Insert(StreamStation);
                    StreamStationRepository.Save();

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
            var StreamStation = StreamStationRepository.GetByID(id);

            if (StreamStation != null)
            {
                var model = StreamStation.ToModel();

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

                    StreamStationRepository.Update(setting);
                    StreamStationRepository.Save();

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