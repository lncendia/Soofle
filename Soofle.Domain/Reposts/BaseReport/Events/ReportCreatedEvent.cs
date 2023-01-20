﻿using Soofle.Domain.Abstractions;
using Soofle.Domain.ReportLogs.Enums;

namespace Soofle.Domain.Reposts.BaseReport.Events;

public class ReportCreatedEvent : IDomainEvent
{
    public ReportCreatedEvent(IEnumerable<Guid> users, Guid reportId, ReportType type, DateTimeOffset createdAt,
        string additionalInfo)
    {
        Users = users.ToList();
        ReportId = reportId;
        Type = type;
        CreatedAt = createdAt;
        AdditionalInfo = additionalInfo;
    }

    public List<Guid> Users { get; }
    public Guid ReportId { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreatedAt { get; }
    public string AdditionalInfo { get; }
}