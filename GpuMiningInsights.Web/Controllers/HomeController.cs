using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GpuMiningInsights.Core;
using OpenQA.Selenium;
using Newtonsoft.Json;
using System.Linq;
using CreaDev.Framework.Core.Utils;
using GpuMiningInsights.Application;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Helpers;

namespace GpuMiningInsights.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? ignoreCache=null)
        {
            var report = LoadReport(ignoreCache);
            return View(report);
        }
        public ActionResult GpuInsightDetails(int id, int? reportId = null)
        {
            var report = LoadReport(true);
            ViewData[Constants.GPU_ID] = id;
            return View(report);
        }

        private static GpusInsightsReport LoadReport(bool? ignoreCache=null)
        {
            GpusInsightsReport report = Caching.LoadChache<GpusInsightsReport>(Constants.GPU_INSIGHTS_REPORT);
            if (report == null || ignoreCache == true)
            {
                report = GpusInsightsReportService.Instance.GetLatestReport();
                Caching.SetCache(Constants.GPU_INSIGHTS_REPORT, report, new TimeSpan(0,20,0));
            }

            return report;
        }
    }
}