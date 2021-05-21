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
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace GpuMiningInsights.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? ignoreCache=null)
        {

            
            var report = LoadReport(ignoreCache);
            
            {
                
            }
            return View(report);
        }
        public ActionResult GpuInsightDetails(int id, int? reportId = null)
        {
            var report = LoadReport(true);
            ViewData[Constants.GPU_ID] = id;
            return View(report);
        }

        private  GpusInsightsReport LoadReport(bool? ignoreCache=null)
        {
            
            bool isIgnoreCache = ignoreCache ?? false;
            isIgnoreCache = isIgnoreCache || Request.IsLocal;
            GpusInsightsReport report = Caching.LoadChache<GpusInsightsReport>(Constants.GPU_INSIGHTS_REPORT);
            if (report == null || isIgnoreCache == true)
            {
                report = GpusInsightsReportService.Instance.GetLatestReport();
                Caching.SetCache(Constants.GPU_INSIGHTS_REPORT, report, new TimeSpan(0,20,0));
            }

            return report;
        } 
        public  ActionResult TestData(bool? ignoreCache=null)
        {
            var baseDir = Assembly.GetExecutingAssembly().Location;
            baseDir = Path.GetDirectoryName(baseDir);

            var filePath = $"{baseDir}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}";

            Process p = new Process();
            p.StartInfo.WorkingDirectory = @"c:\\Windows\\Downloaded Program Files";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.FileName = @"cmd.exe";
            string argument = "amazon-buddy products -k 'vacume cleaner' -n 40 --filetype csv";
            p.StartInfo.Arguments = argument;
            p.Start();

            return HttpNotFound();
        }
    }
}