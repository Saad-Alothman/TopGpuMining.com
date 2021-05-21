using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreaDev.Framework.Core.Resources;
using GpuMiningInsights.Application.Services;

namespace GpuMiningInsights.Web.Helpers
{
    public static class ListItemHelper
    {
        public static List<SelectListItem> GpuList()
        {
            List<SelectListItem> items = new List<SelectListItem>(){new SelectListItem(){Text = "GPU" ,Value = ""}};
            items.AddRange(
            GpuService.Instance.GetAll().Select(s=> new SelectListItem(){Value = s.Id.ToString(),Text = s.Name})
                );

            return items;
        }
    }
}