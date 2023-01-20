using Soofle.Domain.Reposts.LikeReport.Exceptions;
using Soofle.Domain.Reposts.LikeReport.ValueObjects;
using Soofle.Domain.Reposts.PublicationReport.Entities;

namespace Soofle.Domain.Reposts.LikeReport.Entities;

public class LikeReportElement : PublicationReportElement
{
    internal LikeReportElement(string name, string likeChatName, long vkId, Guid participantId, bool vip, string? note,
        LikeReportElement? parent, int id) : base(name, likeChatName, vkId, participantId, vip, note, parent, id)
    {
    }

    public new LikeReportElement? Parent => base.Parent as LikeReportElement;

    private readonly List<LikeInfo> _likes = new();
    public IReadOnlyCollection<LikeInfo> Likes => _likes.AsReadOnly();

    internal void AddLike(LikeInfo like)
    {
        if (_likes.Any(x => x.PublicationId == like.PublicationId))
            throw new LikeAlreadyExistException();
        _likes.Add(like);
    }
}