using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using System.Linq;

namespace GpuMiningInsights.Application.Services
{
    public class GpusInsightsReportService : GmiServiceBase<GpusInsightsReport, GpusInsightsReportService>
    {
        public GpusInsightsReportService()
        {
            Includes = new System.Collections.Generic.List<string>()
            {
                nameof(GpusInsightsReport.GpuInsightReports),
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}.{nameof(Gpu.Model)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}.{nameof(Gpu.Model)}.{nameof(Model.HashRates)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.PriceSourceItems)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.PriceSourceItems)}.{nameof(PriceSourceItem.PriceSource)}"
            };
        }
        public GpusInsightsReport GetLatestReport()
        {
            var last= Search(new SearchCriteria<GpusInsightsReport>()
            {
                SortExpression = s => s.OrderByDescending(r => r.Date)
            }).Result.FirstOrDefault();

            return last;
        }
    }
}
