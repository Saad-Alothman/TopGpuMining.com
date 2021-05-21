using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Core
{
    public class AppSettings
    {
        public static IConfiguration Configuration { get; set; }
        public const string DateFormat = "dd-MM-yyyy   HHmmss";
    }
}
