using VkQ.WEB.ViewModels.Reports.Base;

namespace VkQ.WEB.ViewModels.Reports;

public class ParticipantReportViewModel : ReportViewModel
{
    public ParticipantReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, long vkId) : base(
        id, creationDate, startDate, endDate, isStarted, isCompleted, isSucceeded, message)
    {
        VkId = vkId;
    }

    public long VkId { get; }
}