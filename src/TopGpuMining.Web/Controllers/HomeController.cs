using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopGpuMining.Web.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult TestData(bool? ignoreCache = null)
        {
            string filetype = "json";
            var baseDir = Assembly.GetExecutingAssembly().Location;
            baseDir = Path.GetDirectoryName(baseDir);

            var filePath = $"{baseDir}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}";

            string output = string.Empty;
            Process p = new Process();
            p.StartInfo.WorkingDirectory = filePath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
           
            p.StartInfo.FileName = @"cmd.exe";
            string argument = $"/C amazon-buddy asin B06ZYRRW9T --filetype {filetype} --random-ua";
            //string argument = "/C amazon-buddy products -k 'vacume cleaner' -n 40 --filetype json --random-ua";
            //
            p.StartInfo.Arguments = argument;
            p.OutputDataReceived +=(object sender, DataReceivedEventArgs e) => {
                output+= e.Data;
                };
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();

            string fileName = output.Substring(output.IndexOf(":")+1);
            string result = System.IO.File.ReadAllText($"{filePath}{Path.DirectorySeparatorChar}{fileName}.{filetype}");
            System.IO.File.Delete($"{filePath}{Path.DirectorySeparatorChar}{fileName}.{filetype}");
            //output = p.StandardOutput.ReadToEnd();

            return Ok(result);
        }
    }
}
