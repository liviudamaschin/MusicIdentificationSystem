using MusicIdentificationSystem.AdminLTE.AutoMapperConfig;
using MusicIdentificationSystem.AdminLTE.Helpers;
using MusicIdentificationSystem.AdminLTE.Models.Reports;
using MusicIdentificationSystem.Common;
using MusicIdentificationSystem.DAL;
using MusicIdentificationSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
                    model.ReportType = 0;
                    // 1 - fara group by, cu data start melodie(time report details)
                    // 2 - cu group by  dupa account, stream station si track(time report first aggregation)
                    // 3 - cu group by dupa account(time report maximum aggregation)
                    // 4 - count cu group by dupa account, stream station si track(count report first aggregation)
                    // 5 - cu group by dupa account(count report maximum aggregation)
                    if (timeReportDetails)
                        model.ReportType = 1;
                    else if (timeReportFirstAggregation)
                        model.ReportType = 2;
                    else if (timeReportMaximumAggregation)
                        model.ReportType = 3;
                    else if (countReportFirstAggregation)
                        model.ReportType = 4;
                    else if (countReportMaximumAggregation)
                        model.ReportType = 5;

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

                    model.TimeReportModelsList = reportsRepository.GetTimeReport(model.StartDate, model.EndDate, model.AccountIds, model.StreamStationIds, model.TrackIds, model.ReportType).Select(x => x.ToModel()).ToList();

                    #region TempData (transfer data from Controller to View  for Paging, Filtering and Sorting)
                    var lightModel = new TimeReportListModel()
                    {
                        ReportType = model.ReportType,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        AccountIds = model.AccountIds,
                        StreamStationIds = model.StreamStationIds,
                        TrackIds = model.TrackIds
                    };

                    TempData["model"] = lightModel;
                    #endregion

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

        [HttpGet]
        public ActionResult GenerateReport()
        {
            TimeReportListModel model = null;

            try
            {
                model = TempData["model"] as TimeReportListModel;
            }
            catch (Exception ex)
            {
                model = new TimeReportListModel();
            }

            try
            {
                #region TempData (transfer data from Controller to View  for Paging, Filtering and Sorting)
                var lightModel = new TimeReportListModel()
                {
                    ReportType = model.ReportType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    AccountIds = model.AccountIds,
                    StreamStationIds = model.StreamStationIds,
                    TrackIds = model.TrackIds
                };

                TempData["model"] = lightModel;
                #endregion

                model.TimeReportModelsList = reportsRepository.GetTimeReport(model.StartDate, model.EndDate, model.AccountIds, model.StreamStationIds, model.TrackIds, model.ReportType).Select(x => x.ToModel()).ToList();

                return View("TimeReport", model);
            }
            catch (Exception ex)
            {
                ErrorNotification(Utils.UnwrapException(ex).Message);
                return View("TimeReport", model);
            }
        }

        public ActionResult Download()
        {
            TimeReportListModel model = null;

            try
            {
                model = TempData["model"] as TimeReportListModel;
            }
            catch (Exception ex)
            {
                model = new TimeReportListModel();
            }

            try
            {
                #region TempData (transfer data from Controller to View  for Paging, Filtering and Sorting)
                var lightModel = new TimeReportListModel()
                {
                    ReportType = model.ReportType,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    AccountIds = model.AccountIds,
                    StreamStationIds = model.StreamStationIds,
                    TrackIds = model.TrackIds
                };

                TempData["model"] = lightModel;
                #endregion

                model.TimeReportModelsList = reportsRepository.GetTimeReport(model.StartDate, model.EndDate, model.AccountIds, model.StreamStationIds, model.TrackIds, model.ReportType).Select(x => x.ToModel()).ToList();

                GridView gridView = new GridView();
                gridView.DataSource = model.TimeReportModelsList;
                gridView.DataBind();

                return new DownloadFileActionResult(gridView, string.Format("TimeReport_{0}_{1}.xls", ((ReportType)model.ReportType).ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
            }
            catch (Exception ex)
            {
                ErrorNotification(Utils.UnwrapException(ex).Message);
                return View("TimeReport", model);
            }
        }
    }
}