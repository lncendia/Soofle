namespace VkQ.Domain.Abstractions.DTOs;

public class CommentsDto
{
    public CommentsDto(long publicationId, List<CommentDto> comments)
    {
        PublicationId = publicationId;
        Comments = comments;
    }

    public long PublicationId { get; }
    public List<CommentDto> Comments { get; }
}

public class CommentDto
{
    public CommentDto(long userId, string text)
    {
        UserId = userId;
        Text = text;
    }

    public long UserId { get; }
    public string Text { get; }
}