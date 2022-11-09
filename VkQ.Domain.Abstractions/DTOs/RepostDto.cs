namespace VkQ.Domain.Abstractions.DTOs;

public class RepostsDto
{
    public RepostsDto(long publicationId, List<long> reposts)
    {
        PublicationId = publicationId;
        Reposts = reposts;
    }

    public long PublicationId { get; }
    public List<long> Reposts { get; }
}