using VkQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;
using VkQ.WEB.ViewModels.Reports.Base;

namespace VkQ.WEB.Mappers.Abstractions;

public interface IReportMapperUnit<in TReport, out TViewModel>
    where TReport : ReportDto where TViewModel : ReportViewModel
{
    TViewModel Map(TReport report);
}