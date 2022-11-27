using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.Application.Abstractions.DTO.Reports;

public class ReportDto
{
    public ReportDto(Guid id, string? hashtag, ReportType type, DateTimeOffset creationDate, DateTimeOffset? startDate,
        DateTimeOffset? endDate, bool isCompleted, bool isSucceeded)
    {
        Id = id;
        Hashtag = hashtag;
        Type = type;
        CreationDate = creationDate;
        StartDate = startDate;
        EndDate = endDate;
        IsCompleted = isCompleted;
        IsSucceeded = isSucceeded;
    }

    public Guid Id { get; }
    public string? Hashtag { get; }
    public ReportType Type { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? StartDate { get; }
    public DateTimeOffset? EndDate { get; }
    public bool IsCompleted { get; }
    public bool IsSucceeded { get; }
}