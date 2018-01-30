using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.ApplicationSetting;
using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicIdentificationSystem.AdminLTE.Controllers
{
    public class ApplicationSettingController : BaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("ApplicationSettingList");
        }

        [HttpGet]
        public ActionResult ApplicationSettingList()
        {
            var applicationSettingListModel = new ApplicationSettingListModel();

            var applicationSettingsList = unitOfWork.ApplicationSettingRepository.Get();
            var accountsList = unitOfWork.AccountRepository.Get();

            applicationSettingListModel.ApplicationSettingModelsList = (from AS in applicationSettingsList
                                                                        join A in accountsList on AS.AccountId equals A.Id into AA
                                                                        from ALJ in AA.DefaultIfEmpty()
                                                                        select new ApplicationSettingModel
                                                                        {
                                                                            Id = AS.Id,
                                                                            AccountId = AS.AccountId,
                                                                            KeyName = AS.KeyName,
                                                                            KeyValue = AS.KeyValue,
                                                                            Description = AS.Description,
                                                                            IsActive = AS.IsActive,
                                                                            AccountName = ALJ != null ? ALJ.AccountName : Resources.Resources.Global_Lookup_All
                                                                        }).ToList();

            return View("ApplicationSettingList", applicationSettingListModel);
        }
        #endregion List

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ApplicationSettingModel();
            model.IsActive = true;
            CreateLookups(model);
            return View("CreateApplicationSetting", model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ApplicationSettingModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var setting = model.ToEntity();
                    ApplicationSettingEntity setting = new ApplicationSettingEntity(); // to be deleted

                    if (setting.AccountId == 0)
                        setting.AccountId = null;

                    unitOfWork.ApplicationSettingRepository.Insert(setting);
                    unitOfWork.Save();

                    SuccessNotification(Resources.Resources.ApplicationSetting_SuccessfullyCreated);

                    return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                    CreateLookups(model);
                    return View("CreateApplicationSetting", model);
                }
            }
            CreateLookups(model);
            return View("CreateApplicationSetting", model);
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var setting = unitOfWork.ApplicationSettingRepository.GetByID(id);

            if (setting != null)
            {
                //var model = setting.ToModel();
                ApplicationSettingModel model = new ApplicationSettingModel(); // to be deleted

                if (model.AccountId == null)
                    model.AccountId = 0;

                CreateLookups(model);

                return View("EditApplicationSetting", model);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ApplicationSettingModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var setting = model.ToEntity();
                    ApplicationSettingEntity setting = new ApplicationSettingEntity(); // to be deleted

                    if (setting.AccountId == 0)
                        setting.AccountId = null;

                    unitOfWork.ApplicationSettingRepository.Update(setting);
                    unitOfWork.Save();

                    SuccessNotification(Resources.Resources.ApplicationSetting_SuccessfullyUpdated);
                    return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                    CreateLookups(model);
                    return View("EditSetting", model);
                }
            }
            CreateLookups(model);
            return View("EditSetting", model);
        }

        #endregion

        #region Non-Actions

        [NonAction]
        private void CreateLookups(ApplicationSettingModel model)
        {
            model.AccountsList.Add(new SelectListItem()
            {
                Text = Resources.Resources.Global_Lookup_All,
                Value = Resources.Resources.Global_Lookup_IdZero
            });

            model.AccountsList.AddRange(unitOfWork.AccountRepository.Get().Where(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.AccountName,
                Value = x.Id.ToString()
            }).ToList());
        }

        #endregion
    }
}