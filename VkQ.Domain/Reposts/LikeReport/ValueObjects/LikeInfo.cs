namespace VkQ.Domain.Reposts.LikeReport.ValueObjects;

public class LikeInfo
{
    public LikeInfo(Guid publicationId, bool isLiked, bool isLoaded = true)
    {
        IsLiked = isLiked;
        PublicationId = publicationId;
        IsLoaded = isLoaded;
    }
    
    public Guid PublicationId { get; }
    public bool IsLiked { get; }
    public bool IsLoaded { get; }
}