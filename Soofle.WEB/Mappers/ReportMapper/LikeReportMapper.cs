using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Reports;

namespace Soofle.WEB.Mappers.ReportMapper;

public class LikeReportMapper : IReportMapperUnit<LikeReportDto, LikeReportViewModel>
{
    public LikeReportViewModel Map(LikeReportDto report)
    {
        return new LikeReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.Hashtag, report.SearchStartDate, report.Process, report.PublicationsCount);
    }
}