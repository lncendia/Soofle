namespace VkQ.Application.Abstractions.ReportsManagement.DTOs;

public class LikeReportCreateDto
{
    public LikeReportCreateDto(Guid userId, string hashtag, DateTimeOffset? searchStartDate,
        IEnumerable<Guid>? coAuthors)
    {
        UserId = userId;
        Hashtag = hashtag;
        SearchStartDate = searchStartDate;
        if (coAuthors != null) CoAuthors.AddRange(coAuthors);
    }

    public Guid UserId { get; }
    public string Hashtag { get; }
    public DateTimeOffset? SearchStartDate { get; }
    public List<Guid> CoAuthors { get; } = new();
}