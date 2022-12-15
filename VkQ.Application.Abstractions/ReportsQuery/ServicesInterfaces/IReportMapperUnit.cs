using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapperUnit<out TReportDto, in TReport> where TReportDto : ReportBaseDto where TReport : Report
{
    public TReportDto Map(TReport report);
}