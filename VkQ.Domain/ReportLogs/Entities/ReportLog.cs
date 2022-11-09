using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.Domain.ReportLogs.Entities;

public class ReportLog
{
    public ReportLog(Guid userId, Guid reportId, ReportType type, DateTimeOffset createdAt)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ReportId = reportId;
        Type = type;
        CreatedAt = createdAt;
    }
    
    public Guid Id { get; }
    public Guid UserId { get; }
    public Guid ReportId { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreatedAt { get; }
    
}