namespace VkQ.Domain.Reposts.LikeReport.ValueObjects;

public class LikeReportElement
{
    public LikeReportElement(string name, Guid participantId, List<LikeInfo> likes, List<LikeReportElement> children,
        bool isAccepted)
    {
        Name = name;
        ParticipantId = participantId;
        Likes = likes;
        IsAccepted = isAccepted;
        Children = children;
    }

    public string Name { get; }
    public Guid ParticipantId { get; }
    public List<LikeInfo> Likes { get; }
    public List<LikeReportElement> Children { get; }
    public bool IsAccepted { get; }
}