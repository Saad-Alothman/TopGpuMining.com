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

namespace GpuMiningInsights.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(bool isLoad = true)
        {
            var results = InsighterService.GetInsights();
            ClientGpuListData clientGpuListData = new ClientGpuListData(results, DateTime.Now);
            SaveData(clientGpuListData);
            return View(clientGpuListData);
        }

        public ActionResult Index()
        {
            ClientGpuListData clientGpuListData = LoadClientGpuListData();
            return View(clientGpuListData);
        }
        public ActionResult IndexNew()
        {
            var report =GpusInsightsReportService.Instance.GetLatestReport();
            return View(report);
        }
        
        private ClientGpuListData LoadClientGpuListData(bool loadDummyOnNoData = true)
        {
            ClientGpuListData clientGpuListData = null;
            string fullPath = Server.MapPath(Settings.DataPath);
            List<string> allFiles = System.IO.Directory.GetFiles(fullPath).ToList();
            DateTime dt = DateTime.Now;
            string lastFileName = allFiles
                .Where(r => DateTime.TryParseExact(r.Replace(fullPath, "").Replace("\\", ""), Settings.DateFormat, null,
                    System.Globalization.DateTimeStyles.None, out dt)).OrderByDescending(s =>
                    DateTime.ParseExact(s.Replace(fullPath, "").Replace("\\", ""), Settings.DateFormat, null))
                .FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(lastFileName))
                clientGpuListData =
                    JsonConvert.DeserializeObject<ClientGpuListData>(System.IO.File.ReadAllText(lastFileName));
            else if (allFiles.Select(s => s.Replace(fullPath, "").Replace("\\", "")).Contains(Settings.DummyFileName))
            {
                clientGpuListData =
                    JsonConvert.DeserializeObject<ClientGpuListData>(
                        System.IO.File.ReadAllText(fullPath + "\\" + "dummy.json"));


            }


            return clientGpuListData;
        }

        public ActionResult PushData(string clientGpuListDataJson)
        {
            string result = true.ToString();
            try
            {
                ClientGpuListData clientGpuListData =
                    JsonConvert.DeserializeObject<ClientGpuListData>(clientGpuListDataJson);
                SaveData(clientGpuListData);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return Json(new { message = result });
        }

        private ClientGpuListData SaveData(ClientGpuListData ClientGpuListData)
        {
            DateTime dateTime = DateTime.Now;
            string json = JsonConvert.SerializeObject(ClientGpuListData);
            DateTime date = DateTime.Now;
            if (!DateTime.TryParseExact(ClientGpuListData.Date, Settings.DateFormat, null,
                System.Globalization.DateTimeStyles.None, out date))
                date = DateTime.Now;

            string FullfileName = Server.MapPath(Settings.DataPath) + "/" + date.ToString(Settings.DateFormat);
            System.IO.File.WriteAllText(FullfileName, json);
            return ClientGpuListData;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            string asinNumber = "B06Y15M48C";

            PriceSourceOld priceSource = new PriceSourceOld()
            {
                PriceSourceItemIdentifier = asinNumber,
                PriceSourceAction = AmazonService.SearchItemLookupOperationOld
            };

            GPUOld gpuOld = new GPUOld()
            {
                PriceSources = new List<PriceSourceOld>() { priceSource },

            };

            List<PriceSourceItemOld> priceSourceItems = InsighterService.GetPrice(gpuOld, priceSource);
            return Json(priceSourceItems.FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
    }
}