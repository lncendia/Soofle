﻿using Soofle.Domain.ReportLogs.Enums;

namespace Soofle.WEB.ViewModels.Reports;

public class ReportShortViewModel
{
    public ReportShortViewModel(Guid? id, string? hashtag, ReportType type, DateTimeOffset creationDate,
        DateTimeOffset? endDate, bool isCompleted, bool isSucceeded)
    {
        Id = id;
        Hashtag = hashtag;
        Type = type;
        CreationDate = creationDate;
        EndDate = endDate;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
    }

    public Guid? Id { get; }
    public string? Hashtag { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
}