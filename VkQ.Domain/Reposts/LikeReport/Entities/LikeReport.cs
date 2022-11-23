using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Reposts.BaseReport.DTOs;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;
using VkQ.Domain.Reposts.LikeReport.DTOs;
using VkQ.Domain.Reposts.LikeReport.Exceptions;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.LikeReport.Entities;

public class LikeReport : PublicationReport
{
    public LikeReport(User user, string hashtag, DateTimeOffset? startDate = null,
        IReadOnlyCollection<Guid>? coAuthors = null) : base(user, hashtag, startDate, coAuthors)
    {
    }

    public List<LikeReportElement> Elements => ReportElementsList.Cast<LikeReportElement>().ToList();

    ///<exception cref="ReportAlreadyStartedException">Report already started</exception>
    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ElementsListEmptyException">data collection is empty</exception>
    public void Start(IEnumerable<(IEnumerable<Participant> participants, string chatName)> data,
        IEnumerable<PublicationDto> publications)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);
        var elements = GetElements(data);
        base.Start(publications, elements);
    }

    private static IEnumerable<LikeReportElement> GetElements(
        IEnumerable<(IEnumerable<Participant> participants, string chatName)> data)
    {
        var elements = new List<LikeReportElement>().AsEnumerable();
        elements = data.Aggregate(elements, (current, tuple) => current.Concat(tuple.participants
            .Where(x => x.ParentParticipantId == null)
            .Select(x => new LikeReportElement(x.Name, tuple.chatName, x.VkId, x.Id, tuple.participants
                .Where(y => y.ParentParticipantId == x.Id)
                .Select(y => new LikeReportElement(y.Name, tuple.chatName, y.VkId, y.Id, null))
                .ToList()))));

        return elements;
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="ElementNotFoundException">Element with given id not found</exception>
    ///<exception cref="PublicationNotFoundException">Publication not found</exception>
    ///<exception cref="LikeAlreadyExistException">Like already exist</exception>
    public void SetLike(Guid elementId, LikeDto dto)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);

        var nodes = ReportElementsList.Union(ReportElementsList.Where(x => x.Children.Any())
            .SelectMany(x => x.Children));
        var node = nodes.FirstOrDefault(x => x.Id == elementId);
        if (node == null) throw new ElementNotFoundException();
        var publication = PublicationsList.FirstOrDefault(x => x.ItemId == dto.PostId && x.OwnerId == dto.OwnerId);
        if (publication == null) throw new PublicationNotFoundException();
        ((LikeReportElement)node).AddLike(new LikeInfo(publication.Id, dto.IsLiked, dto.IsLoaded));
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    public void Finish(string? error = null)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);

        foreach (var element in ReportElementsList.Cast<LikeReportElement>())
        {
            if (element.Likes.Count != PublicationsList.Count) throw new ReportNotCompletedException(Id);
            var likes = element.Likes.AsEnumerable();
            if (element.Children.Any())
            {
                if (element.Children.Cast<LikeReportElement>().Any(x => x.Likes.Count != PublicationsList.Count))
                    throw new ReportNotCompletedException(Id);
                likes = likes.Union(element.Children.Cast<LikeReportElement>().SelectMany(x => x.Likes));
            }

            var count = likes.Where(x => x.IsLiked).DistinctBy(x => x.PublicationId).Count();
            if (count == PublicationsList.Count) element.Accept();
        }

        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);
    }
}