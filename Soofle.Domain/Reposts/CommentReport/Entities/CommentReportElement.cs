using Soofle.Domain.Reposts.CommentReport.Exceptions;
using Soofle.Domain.Reposts.CommentReport.ValueObjects;
using Soofle.Domain.Reposts.PublicationReport.Entities;

namespace Soofle.Domain.Reposts.CommentReport.Entities;

public class CommentReportElement : PublicationReportElement
{
    internal CommentReportElement(string name, string likeChatName, long vkId, Guid participantId, bool vip,
        string? note, CommentReportElement? parent, int id) : base(name, likeChatName, vkId, participantId, vip, note,
        parent, id)
    {
    }

    public new CommentReportElement? Parent => base.Parent as CommentReportElement;

    private readonly List<CommentInfo> _comments = new();
    public IReadOnlyCollection<CommentInfo> Comments => _comments.AsReadOnly();

    internal void AddComment(CommentInfo comment)
    {
        if (_comments.Any(x => x.PublicationId == comment.PublicationId))
            throw new CommentAlreadyExistException();
        _comments.Add(comment);
    }
}