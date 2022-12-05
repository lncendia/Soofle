using VkQ.Domain.Abstractions;
using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.Domain.Reposts.BaseReport.Events;

public class ReportFinishedEvent : IDomainEvent
{
    public ReportFinishedEvent(Guid reportId, bool success)
    {
        ReportId = reportId;
        Success = success;
    }

    public Guid ReportId { get; }
    public bool Success { get; }
}