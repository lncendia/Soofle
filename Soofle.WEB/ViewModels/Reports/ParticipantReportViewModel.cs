using Soofle.WEB.ViewModels.Reports.Base;

namespace Soofle.WEB.ViewModels.Reports;

public class ParticipantReportViewModel : ReportViewModel
{
    public ParticipantReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount,
        long vkId) : base(id, creationDate, startDate, endDate, isStarted, isCompleted, isSucceeded, message,
        elementsCount)
    {
        VkId = vkId;
    }

    public long VkId { get; }
}