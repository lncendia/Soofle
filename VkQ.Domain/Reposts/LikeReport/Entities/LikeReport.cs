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
    public void Start(IEnumerable<LikeElementDto> likes, IEnumerable<PublicationDto> publications)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);
        var elements = likes.Select(GetElement);
        base.Start(publications, elements);
    }

    private static LikeReportElement GetElement(LikeElementDto like) =>
        new(like.Name, like.LikeChatName, like.VkId, like.ParticipantId, like.Children?.Select(GetElement));

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="ElementNotFoundException">Element with given id not found</exception>
    ///<exception cref="PublicationNotFoundException">Publication not found</exception>
    ///<exception cref="LikeAlreadyExistException">Like already exist</exception>
    public void SetLike(Guid elementId, LikeDto dto)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);

        var nodes = ReportElementsList.Concat(ReportElementsList.Cast<LikeReportElement>().SelectMany(x => x.Children));
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
            var children = element.Children;
            if (children.Any())
            {
                if (children.Any(x => x.Likes.Count != PublicationsList.Count))
                    throw new ReportNotCompletedException(Id);
                likes = likes.Concat(children.SelectMany(x => x.Likes));
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