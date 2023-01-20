namespace Soofle.Domain.Reposts.CommentReport.DTOs;

public class CommentsDto
{
    public CommentsDto(long publicationId, long ownerId, IReadOnlyCollection<(long, string)> comments)
    {
        PublicationId = publicationId;
        Comments = comments;
        SuccessLoaded = true;
        OwnerId = ownerId;
    }
    public CommentsDto(long publicationId, long ownerId)
    {
        PublicationId = publicationId;
        OwnerId = ownerId;
        SuccessLoaded = false;
    }

    public long OwnerId { get; }
    public long PublicationId { get; }
    public IReadOnlyCollection<(long, string)>? Comments { get; }
    public bool SuccessLoaded { get; }
}