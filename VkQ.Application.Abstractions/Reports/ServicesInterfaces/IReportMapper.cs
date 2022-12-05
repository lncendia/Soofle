using VkQ.Application.Abstractions.Reports.DTOs.Reports.Base.ReportBaseDto;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Application.Abstractions.Reports.ServicesInterfaces;

public interface IReportMapper<out TReportDto, in TReport> where TReportDto : ReportBaseDto where TReport : Report
{
    public TReportDto Map(TReport report);
}