using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.LikeReport.Exceptions;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;

namespace VkQ.Domain.Reposts.LikeReport.Entities;

public class LikeReportElement : PublicationReportElement
{
    public LikeReportElement(int id, string name, long vkId, Guid participantId, IEnumerable<LikeReportElement>? children) : base(
        id, name, vkId, participantId, children?.Cast<PublicationReportElement>().ToList())
    {
    }

    public List<LikeInfo> Likes { get; } = new();


    public void AddLike(LikeInfo like)
    {
        if (Likes.Any(x => x.PublicationId == like.PublicationId))
            throw new LikeAlreadyExistException();
        Likes.Add(like);
    }
}