﻿namespace Soofle.WEB.ViewModels.Reports.Base;

public abstract class ReportViewModel
{
    protected ReportViewModel(Guid id, DateTimeOffset creationDate, DateTimeOffset? startDate, DateTimeOffset? endDate,
        bool isStarted, bool isCompleted, bool isSucceeded, string? message, int elementsCount)
    {
        Id = id;
        CreationDate = creationDate;
        StartDate = startDate;
        EndDate = endDate;
        IsStarted = isStarted;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
        Message = message;
        ElementsCount = elementsCount;
    }

    public Guid Id { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? StartDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsStarted { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
    public string? Message { get; }
    public int ElementsCount { get; }
}