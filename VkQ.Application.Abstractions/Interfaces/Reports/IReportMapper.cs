using VkQ.Application.Abstractions.DTO.Reports.Base.ReportBaseDto;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Application.Abstractions.Interfaces.Reports;

public interface IReportMapper<out TReportDto, in TReport> where TReportDto : ReportBaseDto where TReport : Report
{
    public TReportDto Map(TReport report);
}