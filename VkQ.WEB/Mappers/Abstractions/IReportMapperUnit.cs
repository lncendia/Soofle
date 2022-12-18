using VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.ReportBaseDto;
using VkQ.WEB.ViewModels.Reports;
using VkQ.WEB.ViewModels.Reports.Base;

namespace VkQ.WEB.Mappers.Abstractions;

public interface IReportMapperUnit<in TReport, out TViewModel>
    where TReport : ReportBaseDto where TViewModel : ReportViewModel
{
    TViewModel Map(TReport report);
}