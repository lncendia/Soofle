using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.Domain.ReportLogs.Entities;

public class ReportLog : IAggregateRoot
{
    public ReportLog(Guid userId, Guid reportId, ReportType type, DateTimeOffset createdAt, string additionalInfo)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ReportId = reportId;
        Type = type;
        CreatedAt = createdAt;
        AdditionalInfo = additionalInfo;
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid ReportId { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? FinishedAt { get; private set; }
    public bool? Success { get; private set; }
    public string AdditionalInfo { get; }

    public void Finish(bool success)
    {
        Success = success;
        FinishedAt = DateTimeOffset.Now;
    }
}