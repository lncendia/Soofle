namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.ReportDto;

public abstract class ReportBuilder
{
    public Guid? Id;
    public DateTimeOffset? CreationDate;
    public DateTimeOffset? StartDate;
    public DateTimeOffset? EndDate;
    public bool IsStarted;
    public bool IsCompleted;
    public bool IsSucceeded;
    public string? Message;
    public int ElementsCount;

    public ReportBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public ReportBuilder WithDates(DateTimeOffset startDate, DateTimeOffset? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
        return this;
    }
    
    public ReportBuilder WithCreationDate(DateTimeOffset creationDate)
    {
        CreationDate = creationDate;
        return this;
    }

    public ReportBuilder WithStatus(bool isCompleted, bool isSucceeded)
    {
        IsStarted = true;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
        return this;
    }

    public ReportBuilder WithMessage(string? message)
    {
        Message = message;
        return this;
    }
    public ReportBuilder WithElements(int count)
    {
        ElementsCount = count;
        return this;
    }
}