using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;
using VkQ.Domain.Reposts.LikeReport.DTOs;
using VkQ.Domain.Reposts.LikeReport.ValueObjects;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.LikeReport.Entities;

public class LikeReport : PublicationReport
{
    public LikeReport(User user, string hashtag, DateTimeOffset? startDate = null) : base(user, hashtag, startDate)
    {
    }

    public List<LikeReportElement>? Elements => ReportElementsList?.Cast<LikeReportElement>().ToList();


    public void LoadElements(IEnumerable<Participant> participant)
    {
        var distinctParticipants = participant.DistinctBy(x => x.Id).ToList();
        int id = 0;
        var nodes = distinctParticipants.Where(x => x.ParentParticipantId == null).Select(x =>
            new LikeReportElement(id++, x.Name, x.VkId, x.Id,
                distinctParticipants.Where(y => y.ParentParticipantId == x.Id)
                    .Select(y => new LikeReportElement(id++, y.Name, y.VkId, y.Id, null)).ToList()));
        LoadElements(nodes);
    }

    public void SetLike(int elementId, LikeDto dto)
    {
        if (!IsInitialized) throw new ReportNotInitializedException();
        if (!IsStarted) throw new ReportNotStartedException();
        if (IsCompleted) throw new ReportAlreadyCompletedException();
        
        var nodes = ReportElementsList!.Union(ReportElementsList!.Where(x => x.Children != null)
            .SelectMany(x => x.Children!));
        var node = nodes.FirstOrDefault(x => x.Id == elementId);
        if (node == null) throw new ElementNotFoundException();
        var publication = PublicationsList?.FirstOrDefault(x => x.ItemId == dto.PostId && x.OwnerId == dto.OwnerId);
        if (publication == null) throw new PublicationNotFoundException();
        ((LikeReportElement)node).AddLike(new LikeInfo(publication.Id, dto.IsLiked, dto.IsLoaded));
    }

    public void Finish(string? error = null)
    {
        if (!IsInitialized) throw new ReportNotInitializedException();
        foreach (var element in ReportElementsList!.Cast<LikeReportElement>())
        {
            if (element.Likes.Count != PublicationsList!.Count) throw new ReportNotCompletedException();
            var likes = element.Likes.AsEnumerable();
            if (element.Children != null)
            {
                if (element.Children.Cast<LikeReportElement>().Any(x => x.Likes.Count != PublicationsList.Count))
                    throw new ReportNotCompletedException();
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