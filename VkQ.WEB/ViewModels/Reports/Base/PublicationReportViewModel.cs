namespace VkQ.WEB.ViewModels.Reports.Base;

public abstract class PublicationReportViewModel : ReportViewModel
{
    protected PublicationReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message,
        List<Guid> linkedUsers, string hashtag, DateTimeOffset? searchStartDate,
        List<PublicationViewModel> publications) : base(id, creationDate, startDate, endDate, isStarted, isCompleted,
        isSucceeded, message)
    {
        LinkedUsers = linkedUsers;
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
        Publications = publications;
    }

    public List<Guid> LinkedUsers { get; }
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }
    public List<PublicationViewModel> Publications { get; }
}