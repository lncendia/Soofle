﻿namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.ReportBaseDto;

public abstract class ReportBaseBuilder
{
    public Guid? Id;
    public DateTimeOffset? CreationDate;
    public DateTimeOffset? StartDate;
    public DateTimeOffset? EndDate;
    public bool IsStarted;
    public bool IsCompleted;
    public bool IsSucceeded;
    public string? Message;

    public ReportBaseBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public ReportBaseBuilder WithDates(DateTimeOffset startDate, DateTimeOffset? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
        return this;
    }
    
    public ReportBaseBuilder WithCreationDate(DateTimeOffset creationDate)
    {
        CreationDate = creationDate;
        return this;
    }

    public ReportBaseBuilder WithStatus(bool isCompleted, bool isSucceeded)
    {
        IsStarted = true;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
        return this;
    }

    public ReportBaseBuilder WithMessage(string? message)
    {
        Message = message;
        return this;
    }
}