using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.ApplicationSetting;
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
    public class ApplicationSettingController : BaseController
    {
        private ApplicationSettingRepository applicationSettingRepository = new ApplicationSettingRepository();
        private AccountRepository accountRepository = new AccountRepository();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("ApplicationSettingList");
        }

        [HttpGet]
        public ActionResult ApplicationSettingList()
        {
            var applicationSettingListModel = new ApplicationSettingListModel();

            var applicationSettingsList = applicationSettingRepository.Get();
            var accountsList = accountRepository.Get();

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
                    var setting = model.ToEntity();

                    if (setting.AccountId == 0)
                        setting.AccountId = null;

                    applicationSettingRepository.Insert(setting);
                    applicationSettingRepository.Save();

                    SuccessNotification(Resources.Resources.ApplicationSetting_SuccessfullyCreated);

                    return continueEditing ? RedirectToAction("Edit", new { id = setting.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    CreateLookups(model);
                    return View("CreateApplicationSetting", model);
                }
            }
            else
            {
                CreateLookups(model);
                return View("CreateApplicationSetting", model);
            }
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var setting = applicationSettingRepository.GetByID(id);

            if (setting != null)
            {
                var model = setting.ToModel();

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
                    var setting = model.ToEntity();

                    if (setting.AccountId == 0)
                        setting.AccountId = null;

                    applicationSettingRepository.Update(setting);
                    applicationSettingRepository.Save();

                    SuccessNotification(Resources.Resources.ApplicationSetting_SuccessfullyUpdated);
                    return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    CreateLookups(model);
                    return View("EditApplicationSetting", model);
                }
            }
            else
            {
                CreateLookups(model);
                return View("EditApplicationSetting", model);
            }
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

            model.AccountsList.AddRange(accountRepository.Get().Where(x => x.IsActive.Value).Select(x => new SelectListItem()
            {
                Text = x.AccountName,
                Value = x.Id.ToString()
            }).ToList());
        }

        #endregion
    }
}