using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.LikeReport.DTOs;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.LikeReport;

public class LikeReport : PublicationReport
{
    public LikeReport(User user) : base(user)
    {
    }

    private readonly List<LikeReportElement> _likes = new();
    public List<LikeReportElement> Elements => _likes.ToList();

    public void AddElement(ElementDto dto)
    {
        ValidateElement(dto);
        _likes.Add(MapElement(dto));
    }

    private void ValidateElement(ElementDto dto)
    {
        if (PublicationsList == null) throw new PublicationsListNotSetException();
        if (dto.Children.Any(x => x.Children.Any())) throw new ChildElementException();

        var newPublications = dto.Values.Select(x => x.postId);
        newPublications = dto.Children.Aggregate(newPublications,
                (current, child) => current.Union(child.Values.Select(x => x.postId)))
            .Distinct();
        var publications = PublicationsList.Select(x => x.Id);
        if (newPublications.Except(publications).Any()) throw new PublicationNotFoundException();

        var newParticipants = dto.Children.Select(x => x.ParticipantId);
        newParticipants = newParticipants.Append(dto.ParticipantId);
        var oldParticipants =
            Elements.SelectMany(x => x.Children.Select(element => element.ParticipantId).Append(x.ParticipantId));
        var participant = oldParticipants.Intersect(newParticipants).FirstOrDefault();
        if (participant != default) throw new ReportElementAlreadyExistException(participant);
    }

    private LikeReportElement MapElement(ElementDto dto)
    {
        var likedPosts = dto.Children.Aggregate(dto.Values, (current, child) => current.Union(child.Values))
            .Where(x => x.isLiked).DistinctBy(x => x.postId);
        var accepted = likedPosts.Count() == PublicationsList!.Count;

        return new LikeReportElement(dto.Name, dto.ParticipantId,
            dto.Values.Select(x => new LikeInfo(x.postId, x.isLiked)).ToList(),
            dto.Children.Select(MapElement).ToList(), accepted);
    }
}