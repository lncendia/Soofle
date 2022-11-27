namespace VkQ.Domain.Reposts.LikeReport.DTOs;

public class LikeElementDto
{
    public LikeElementDto(string name, string likeChatName, long vkId, Guid participantId,
        IEnumerable<LikeElementDto>? children)
    {
        Name = name;
        LikeChatName = likeChatName;
        VkId = vkId;
        ParticipantId = participantId;
        Children = children;
    }

    public string Name { get; }
    public string LikeChatName { get; }
    public long VkId { get; }
    public Guid ParticipantId { get; }
    public IEnumerable<LikeElementDto>? Children { get; }
}