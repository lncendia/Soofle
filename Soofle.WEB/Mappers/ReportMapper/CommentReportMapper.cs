using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Reports;

namespace Soofle.WEB.Mappers.ReportMapper;

public class CommentReportMapper : IReportMapperUnit<CommentReportDto, CommentReportViewModel>
{
    public CommentReportViewModel Map(CommentReportDto report)
    {
        return new CommentReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.ElementsCount,
            report.Hashtag, report.SearchStartDate, report.Process, report.PublicationsCount);
    }
}