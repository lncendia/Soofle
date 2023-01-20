using Soofle.Application.Abstractions.ReportsQuery.DTOs.ReportDto;
using Soofle.WEB.ViewModels.Reports.Base;

namespace Soofle.WEB.Mappers.Abstractions;

public interface IReportMapperUnit<in TReport, out TViewModel>
    where TReport : ReportDto where TViewModel : ReportViewModel
{
    TViewModel Map(TReport report);
}