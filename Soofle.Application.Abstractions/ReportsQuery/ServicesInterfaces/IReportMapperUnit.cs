using Soofle.Application.Abstractions.ReportsQuery.DTOs.ReportDto;
using Soofle.Domain.Reposts.BaseReport.Entities;

namespace Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportMapperUnit<out TReportDto, in TReport> where TReportDto : ReportDto where TReport : Report
{
    public TReportDto Map(TReport report);
}