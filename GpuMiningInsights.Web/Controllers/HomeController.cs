using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GpuMiningInsights.Core;
using OpenQA.Selenium;
using Newtonsoft.Json;
using System.Linq;
using GpuMiningInsights.Application;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Helpers;

namespace GpuMiningInsights.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var report = GpusInsightsReportService.Instance.GetLatestReport();
            return View(report);
        }
        public ActionResult GpuInsightDetails(int id, int? reportId = null)
        {
            GpusInsightsReport report = GpusInsightsReportService.Instance.GetLatestReport();
            ViewData[Constants.GPU_ID] = id;
            return View(report);
        }
    }
}