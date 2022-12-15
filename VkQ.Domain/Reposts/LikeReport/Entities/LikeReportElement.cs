using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.LikeReport.Exceptions;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;

namespace VkQ.Domain.Reposts.LikeReport.Entities;

public class LikeReportElement : PublicationReportElement
{
    internal LikeReportElement(string name, string likeChatName, long vkId, Guid participantId, bool vip,
        LikeReportElement? parent) : base(name, likeChatName, vkId, participantId, vip, parent)
    {
    }

    public new LikeReportElement? Parent => base.Parent as LikeReportElement;

    public List<LikeInfo> Likes { get; } = new();

    internal void AddLike(LikeInfo like)
    {
        if (Likes.Any(x => x.PublicationId == like.PublicationId))
            throw new LikeAlreadyExistException();
        Likes.Add(like);
    }
}