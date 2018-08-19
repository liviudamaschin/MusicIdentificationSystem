using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.Account;
using MusicIdentificationSystem.AdminLTE.Models.Reports;
using MusicIdentificationSystem.AdminLTE.Models.Track;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.DAL.DbEntities;
using MusicIdentificationSystem.DAL.Repositories;
using MusicIdentificationSystem.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicIdentificationSystem.AdminLTE.Controllers
{
    public class ReportsController : BaseController
    {
        private TrackRepository trackRepository = new TrackRepository();
        private AccountXTrackRepository accountXTrackRepository = new AccountXTrackRepository();
        private AccountRepository accountRepository = new AccountRepository();
        private StreamStationRepository streamStationRepository = new StreamStationRepository();
        private ReportsRepository reportsRepository = new ReportsRepository();

        public ActionResult List()
        {
            return RedirectToAction("TimeReportList");
        }

        [HttpGet]
        public ActionResult TimeReportList()
        {
            try
            {
                var timeReportListModel = new TimeReportListModel();

                timeReportListModel.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                timeReportListModel.EndDate = timeReportListModel.StartDate.AddMonths(1).AddDays(-1);

                return View("TimeReport", timeReportListModel);
            }
            catch (Exception ex)
            {
                ErrorNotification(Utils.UnwrapException(ex).Message);
                return View("TimeReport", null);
            }
        }

        [HttpGet]
        public ActionResult GetAccountsList()
        {
            try
            {
                var accountsList = accountRepository.Get();

                var result = accountsList.Select(x => x.ToModel()).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(Utils.UnwrapException(ex).Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStreamStationsList()
        {
            try
            {
                var streamStationsList = streamStationRepository.Get();

                var result = streamStationsList.Select(x => x.ToModel()).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(Utils.UnwrapException(ex).Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTracksList()
        {
            try
            {
                var tracksList = trackRepository.Get();

                var result = tracksList.Select(x => x.ToModel()).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(Utils.UnwrapException(ex).Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ParameterBasedOnFormName("time-report-details", "timeReportDetails")]
        [ParameterBasedOnFormName("time-report-first-aggregation", "timeReportFirstAggregation")]
        [ParameterBasedOnFormName("time-report-maximum-aggregation", "timeReportMaximumAggregation")]
        [ParameterBasedOnFormName("count-report-first-aggregation", "countReportFirstAggregation")]
        [ParameterBasedOnFormName("count-report-maximum-aggregation", "countReportMaximumAggregation")]
        public ActionResult GenerateReport(TimeReportListModel model, bool timeReportDetails, bool timeReportFirstAggregation, bool timeReportMaximumAggregation, bool countReportFirstAggregation, bool countReportMaximumAggregation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var reportType = 0;
                    // 1 - fara group by, cu data start melodie(time report details)
                    // 2 - cu group by  dupa account, stream station si track(time report first aggregation)
                    // 3 - cu group by dupa account(time report maximum aggregation)
                    // 4 - count cu group by dupa account, stream station si track(count report first aggregation)
                    // 5 - cu group by dupa account(count report maximum aggregation)
                    if (timeReportDetails)
                        reportType = 1;
                    else if (timeReportFirstAggregation)
                        reportType = 2;
                    else if (timeReportMaximumAggregation)
                        reportType = 3;
                    else if (countReportFirstAggregation)
                        reportType = 4;
                    else if (countReportMaximumAggregation)
                        reportType = 5;

                    var startDate = DateTime.MinValue;
                    var endDate = DateTime.MinValue;
                    var dates = model.Period.Split('-');
                    if (dates != null && dates.Length == 2)
                    {
                        startDate = DateTime.ParseExact(dates[0].Trim(), Constants.AdminLTE_DateTimeFormat_ForC, CultureInfo.InvariantCulture, DateTimeStyles.None);
                        endDate = DateTime.ParseExact(dates[1].Trim(), Constants.AdminLTE_DateTimeFormat_ForC, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    }
                    model.StartDate = startDate;
                    model.EndDate = endDate;
                    model.TimeReportModelsList = reportsRepository.GetTimeReport(startDate, endDate, model.AccountIds, model.StreamStationIds, model.TrackIds, reportType).Select(x => x.ToModel()).ToList();

                    return View("TimeReport", model);
                }
                catch (Exception ex)
                {
                    ErrorNotification(Utils.UnwrapException(ex).Message);
                    return View("TimeReport", model);
                }
            }
            else
            {
                return View("TimeReport", model);
            }
        }
    }
}