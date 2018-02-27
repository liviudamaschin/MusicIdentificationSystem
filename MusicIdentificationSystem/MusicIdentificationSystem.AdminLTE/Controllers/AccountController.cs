using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.Account;
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
    public class AccountController : BaseController
    {
        private AccountRepository accountRepository = new AccountRepository();

        #region List
        public ActionResult List()
        {
            return RedirectToAction("AccountList");
        }

        [HttpGet]
        public ActionResult AccountList()
        {
            var accountListModel = new AccountListModel();

            var accountsList = accountRepository.Get();

            accountListModel.AccountModelsList = accountsList.Select(x => x.ToModel()).ToList();

            return View("AccountList", accountListModel);
        }
        #endregion List

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AccountModel();
            model.IsActive = true;
            return View("CreateAccount", model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(AccountModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = model.ToEntity();

                    accountRepository.Insert(account);
                    accountRepository.Save();

                    SuccessNotification(Resources.Resources.Account_SuccessfullyCreated);

                    return continueEditing ? RedirectToAction("Edit", new { id = account.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("CreateAccount", model);
                }
            }
            else
            {
                return View("CreateAccount", model);
            }
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var account = accountRepository.GetByID(id);

            if (account != null)
            {
                var model = account.ToModel();

                return View("EditAccount", model);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(AccountModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var setting = model.ToEntity();

                    accountRepository.Update(setting);
                    accountRepository.Save();

                    SuccessNotification(Resources.Resources.Account_SuccessfullyUpdated);
                    return continueEditing ? RedirectToAction("Edit", new { id = model.Id }) : RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("EditAccount", model);
                }
            }
            else
            {
                return View("EditAccount", model);
            }
        }

        #endregion
    }
}