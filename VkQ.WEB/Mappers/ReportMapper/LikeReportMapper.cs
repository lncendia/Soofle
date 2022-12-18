using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.ViewModels.Reports;

namespace VkQ.WEB.Mappers.ReportMapper;

public class LikeReportMapper : IReportMapperUnit<LikeReportDto, LikeReportViewModel>
{
    public LikeReportViewModel Map(LikeReportDto report)
    {
        var publications = report.Publications.Select(x => new PublicationViewModel(x.Id, x.ItemId, x.OwnerId))
            .ToList();
        return new LikeReportViewModel(report.Id, report.CreationDate, report.StartDate, report.EndDate,
            report.IsStarted, report.IsCompleted, report.IsSucceeded, report.Message, report.LinkedUsers,
            report.Hashtag, report.SearchStartDate, publications);
    }
}