namespace VkQ.Domain.Reposts.LikeReport.DTOs;

public class ElementDto
{
    public ElementDto(string name, Guid participantId, IEnumerable<(int postId, bool isLiked)> values,
        IEnumerable<ElementDto> children)
    {
        Name = name;
        ParticipantId = participantId;
        Children = children;
        Values = values;
    }

    public string Name { get; }
    public Guid ParticipantId { get; }
    public IEnumerable<ElementDto> Children { get; }
    public IEnumerable<(int postId, bool isLiked)> Values { get; }
}