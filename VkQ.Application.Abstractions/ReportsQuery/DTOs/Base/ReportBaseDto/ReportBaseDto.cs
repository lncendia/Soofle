namespace VkQ.Application.Abstractions.ReportsQuery.DTOs.Base.ReportBaseDto;

public abstract class ReportBaseDto
{
    protected ReportBaseDto(ReportBaseBuilder builder)
    {
        Id = builder.Id ?? throw new ArgumentException("builder not formed", nameof(builder));
        CreationDate = builder.CreationDate ?? throw new ArgumentException("builder not formed", nameof(builder));
        StartDate = builder.StartDate;
        EndDate = builder.EndDate;
        IsStarted = builder.IsStarted;
        IsCompleted = builder.IsCompleted;
        IsSucceeded = builder.IsSucceeded;
        Message = builder.Message;
    }

    public Guid Id { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? StartDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsStarted { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
    public string? Message { get; }
}