using VkQ.WEB.ViewModels.Reports.Base;

namespace VkQ.WEB.ViewModels.Reports;

public class LikeReportViewModel : PublicationReportViewModel
{
    public LikeReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate, DateTimeOffset? endDate,
        bool isStarted, bool isCompleted, bool isSucceeded, string? message, List<Guid> linkedUsers, string hashtag,
        DateTimeOffset? searchStartDate, List<PublicationViewModel> publications) : base(id, creationDate, startDate,
        endDate, isStarted, isCompleted, isSucceeded, message, linkedUsers, hashtag, searchStartDate, publications)
    {
    }
}