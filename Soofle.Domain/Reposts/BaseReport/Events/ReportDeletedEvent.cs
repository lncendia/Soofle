using Soofle.Domain.Abstractions;

namespace Soofle.Domain.Reposts.BaseReport.Events;

public class ReportDeletedEvent : IDomainEvent
{
    public ReportDeletedEvent(Guid reportId)
    {
        ReportId = reportId;
    }
    
    public Guid ReportId { get; }
}