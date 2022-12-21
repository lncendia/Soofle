using VkQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;
using VkQ.Domain.Reposts.BaseReport.Entities;

namespace VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapperUnit<out TReportDto, in TReport> where TReportDto : ReportDto where TReport : Report
{
    public TReportDto Map(TReport report);
}