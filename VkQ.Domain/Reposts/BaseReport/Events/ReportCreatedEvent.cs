using VkQ.Domain.Abstractions;
using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.Domain.Reposts.BaseReport.Events;

public class ReportCreatedEvent : IDomainEvent
{
    public ReportCreatedEvent(Guid userId, Guid reportId, ReportType type, DateTimeOffset createdAt,
        string additionalInfo)
    {
        UserId = userId;
        ReportId = reportId;
        Type = type;
        CreatedAt = createdAt;
        AdditionalInfo = additionalInfo;
    }

    public Guid UserId { get; }
    public Guid ReportId { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreatedAt { get; }
    public string AdditionalInfo { get; }
}