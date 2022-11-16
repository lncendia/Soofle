namespace VkQ.Domain.Reposts.LikeReport.DTOs;

public class LikeDto
{
    public LikeDto(long postId, long ownerId, bool isLiked, bool isLoaded = true)
    {
        PostId = postId;
        OwnerId = ownerId;
        IsLiked = isLiked;
        IsLoaded = isLoaded;
    }

    public long PostId { get; }
    public long OwnerId { get; }
    public bool IsLiked { get; }
    public bool IsLoaded { get; }
}