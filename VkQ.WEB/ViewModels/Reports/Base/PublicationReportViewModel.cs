namespace VkQ.WEB.ViewModels.Reports.Base;

public abstract class PublicationReportViewModel : ReportViewModel
{
    protected PublicationReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        string hashtag, DateTimeOffset? searchStartDate) : base(id, creationDate, startDate, endDate, isStarted,
        isCompleted, isSucceeded, message, elementsCount)
    {
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
    }

    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }
}