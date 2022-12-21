using VkQ.WEB.ViewModels.Reports.Base;

namespace VkQ.WEB.ViewModels.Reports;

public class LikeReportViewModel : PublicationReportViewModel
{
    public LikeReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate, DateTimeOffset? endDate,
        bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount, string hashtag,
        DateTimeOffset? searchStartDate) : base(id, creationDate, startDate,
        endDate, isStarted, isCompleted, isSucceeded, message, elementsCount, hashtag, searchStartDate)
    {
    }
}