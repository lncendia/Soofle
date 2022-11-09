namespace VkQ.Domain.Abstractions.DTOs;

public class LikesDto
{
    public LikesDto(long publicationId, List<long> likes)
    {
        PublicationId = publicationId;
        Likes = likes;
    }

    public long PublicationId { get; }
    public List<long> Likes { get; }
}