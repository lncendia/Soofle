using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.PublicationReport.Entities;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base;
using VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace VkQ.Infrastructure.DataStorage.Mappers.StaticMethods;

internal static class ReportModelInitializer
{
    internal static void InitPublicationReportModel(PublicationReportModel report, PublicationReport element)
    {
        if (!report.Publications.Any())
        {
            report.Publications = element.Publications.Select(x => new PublicationModel
                { Id = x.Id, ItemId = x.ItemId, OwnerId = x.OwnerId }).ToList();
        }

        report.Hashtag = element.Hashtag;
        report.Process = element.Process;
        report.SearchStartDate = element.SearchStartDate;
        InitReportModel(report, element);
    }

    internal static void InitReportModel(ReportModel report, Report element)
    {
        report.Id = element.Id;
        report.Message = element.Message;
        report.UserId = element.UserId;
        report.CreationDate = element.CreationDate;
        report.EndDate = element.EndDate;
        report.IsCompleted = element.IsCompleted;
        report.IsStarted = element.IsStarted;
        report.IsSucceeded = element.IsSucceeded;
        report.StartDate = element.StartDate;
    }
}