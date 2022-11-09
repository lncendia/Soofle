namespace VkQ.Domain.Reposts.LikeReport.ValueObjects;

public class LikeInfo
{
    public LikeInfo(int publicationId, bool isLiked)
    {
        PublicationId = publicationId;
        IsLiked = isLiked;
    }

    public int PublicationId { get; }
    public bool IsLiked { get; }
}