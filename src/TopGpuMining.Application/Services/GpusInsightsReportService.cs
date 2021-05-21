
using TopGpuMining.Domain.Models;
using System.Linq;
using TopGpuMining.Core.Search;
using TopGpuMining.Core.Interfaces;

namespace TopGpuMining.Application.Services
{
    public class GpusInsightsReportService : GenericService<GpusInsightsReport>
    {

        public GpusInsightsReportService(IGenericRepository repository) : base(repository)
        {
            Includes = new[]
            {
                nameof(GpusInsightsReport.GpuInsightReports),
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}.{nameof(Gpu.Model)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}.{nameof(Gpu.Model)}.{nameof(Model.HashRates)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}.{nameof(Gpu.Model)}.{nameof(Model.HashRates)}.{nameof(Hashrate.Algorithm)}",
                $"{nameof(GpusInsightsReport.GpuInsightReports)}.{nameof(GpuInsightReport.Gpu)}.{nameof(Gpu.Model)}.{nameof(Model.HashRates)}.{nameof(Hashrate.Model)}",
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
