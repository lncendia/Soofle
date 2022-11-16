namespace VkQ.Domain.Reposts.LikeReport.ValueObjects;

public class LikeInfo
{
    public LikeInfo(int publicationId, bool isLiked, bool isLoaded = true)
    {
        IsLiked = isLiked;
        PublicationId = publicationId;
        IsLoaded = isLoaded;
    }
    
    public int PublicationId { get; }
    public bool IsLiked { get; }
    public bool IsLoaded { get; }
}