using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GpuMiningInsights.Console;
using GpuMiningInsights.Core;
using OpenQA.Selenium;
using Newtonsoft.Json;
using System.Linq;

namespace GpuMiningInsights.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(bool isLoad = true)
        {
            var results = InsighterService.GetInsights();
           var clientGpuListData= SaveData(results);
            return View(clientGpuListData);
        }
        public ActionResult Index()
        {
            ClientGpuListData clientGpuListData = LoadClientGpuListData();
            return View(clientGpuListData);
        }

        private ClientGpuListData LoadClientGpuListData(bool loadDummyOnNoData =true)
        {
            ClientGpuListData clientGpuListData = null;
            string fullPath = Server.MapPath(Settings.DataPath);
            List<string> allFiles = System.IO.Directory.GetFiles(fullPath).ToList();
            DateTime dt = DateTime.Now;
            string lastFileName = allFiles.Where(r=> DateTime.TryParseExact(r.Replace(fullPath,"").Replace("\\",""), Settings.DateFormat, null,System.Globalization.DateTimeStyles.None, out dt)).OrderByDescending(s => DateTime.ParseExact(s, Settings.DateFormat, null)).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(lastFileName))
                clientGpuListData = JsonConvert.DeserializeObject<ClientGpuListData>(System.IO.File.ReadAllText(lastFileName));
            else if(allFiles.Select(s=>s.Replace(fullPath, "").Replace("\\", "")).Contains("dummy.json")){
                clientGpuListData = JsonConvert.DeserializeObject<ClientGpuListData>(System.IO.File.ReadAllText(fullPath + "\\" + "dummy.json"));

                
            }

            
            return clientGpuListData;
        }

        public ActionResult PushData(List<GPU> gpus)
        {
            SaveData(gpus);
            return Json(true);
        }

        private ClientGpuListData SaveData(List<GPU> gpus)
        {
            DateTime dateTime = DateTime.Now;
            ClientGpuListData ClientGpuListData = new ClientGpuListData()
            {
                Gpus = gpus,
                Date = dateTime.ToString(Settings.DateFormat)
            };
            string json = JsonConvert.SerializeObject(gpus);
            System.IO.File.WriteAllText(Server.MapPath(Settings.DataPath), json);
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
    }
}