using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.Dashboard;
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
    public class DashboardController : BaseController
    {
        private StreamStationRepository streamStationRepository = new StreamStationRepository();
        private AccountRepository accountRepository = new AccountRepository();
        private AccountXTrackRepository accountXTrackRepository = new AccountXTrackRepository();
        private StreamStationXTrackRepository streamStationXTrackRepository = new StreamStationXTrackRepository();

        #region Dashboard
        public ActionResult Dashboard()
        {
            return RedirectToAction("StreamStationStatusList");
        }

        [HttpGet]
        public ActionResult StreamStationStatusList()
        {
            var streamStationStatusListModel = new StreamStationStatusListModel();

            var streamStationsList = streamStationRepository.Get();

            streamStationStatusListModel.StreamStationStatusModelsList = streamStationsList.OrderByDescending(x => x.IsActive)
                                                                                            .ThenBy(x => x.Running)
                                                                                            .ThenBy(x => x.StationName)
                                                                                            .Select(x => x.ToStatusModel())
                                                                                            .ToList();

            return View("StreamStationStatusList", streamStationStatusListModel);
        }

        [HttpGet]
        public ActionResult AccountXStreamStationStatusList(int accountId)
        {
            var account = accountRepository.GetByID(accountId);

            ViewBag.AccountName = account != null ? account.AccountName : null;

            var streamStationStatusListModel = new StreamStationStatusListModel();

            var streamStationsList = streamStationRepository.Get(x => x.IsActive == true);

            var accountXTracksList = accountXTrackRepository.Get(x => x.AccountId == accountId && x.IsActive == true);

            var streamStationXTracksList = streamStationXTrackRepository.Get(x => x.IsActive == true);

            streamStationStatusListModel.StreamStationStatusModelsList = (from at in accountXTracksList
                                                                          join st in streamStationXTracksList
                                                                          on at.TrackId equals st.TrackId
                                                                          join s in streamStationsList
                                                                          on st.StreamStationId equals s.Id
                                                                          select s).Distinct()
                                                                                   .OrderByDescending(x => x.IsActive)
                                                                                   .ThenBy(x => x.Running)
                                                                                   .ThenBy(x => x.StationName)
                                                                                   .Select(x => x.ToStatusModel())
                                                                                   .ToList();

            return View("StreamStationStatusList", streamStationStatusListModel);
        }
        #endregion List
    }
}