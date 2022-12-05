using VkQ.Domain.Participants.Entities;
using VkQ.Domain.ReportLogs.Enums;
using VkQ.Domain.Reposts.BaseReport.DTOs;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.BaseReport.Events;
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
        AddDomainEvent(new ReportCreatedEvent(UserId, Id, ReportType.Likes, CreationDate, Hashtag));
    }

    public List<LikeReportElement> Elements => ReportElementsList.Cast<LikeReportElement>().ToList();
    public int Process { get; private set; } //todo: store

    ///<exception cref="ReportAlreadyStartedException">Report already started</exception>
    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ParticipantNotLinkedToReportException"></exception>
    public void Start(IEnumerable<ParticipantsDto> participants, IEnumerable<PublicationDto> publications)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);
        var elements = new List<LikeReportElement>();

        foreach (var participantsDto in participants)
        {
            ProcessLikeChat(participantsDto.Participants.DistinctBy(x => x.Id).ToList(), participantsDto.LikeChatName,
                elements);
        }

        base.Start(publications, elements);
    }

    private void ProcessLikeChat(IList<Participant> participants, string likeChatName, List<LikeReportElement> elements)
    {
        var p = participants.FirstOrDefault(x => x.UserId != UserId && !LinkedUsers.Contains(x.UserId));
        if (p != null) throw new ParticipantNotLinkedToReportException(p.Id);

        var childElements = participants.Where(x => x.ParentParticipantId.HasValue).GroupBy(x => x.ParentParticipantId)
            .ToList();
        foreach (var participant in participants.Where(x => !x.ParentParticipantId.HasValue))
        {
            var item = new LikeReportElement(participant.Name, likeChatName, participant.VkId, participant.Id, null);
            elements.Add(item);
            var children = childElements.FirstOrDefault(x => x.Key == participant.Id);
            if (children == null) continue;
            elements.AddRange(children.Select(x =>
                new LikeReportElement(x.Name, likeChatName, x.VkId, x.Id, item)));
        }
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="ElementNotFoundException">Element with given id not found</exception>
    ///<exception cref="PublicationNotFoundException">Publication not found</exception>
    ///<exception cref="LikeAlreadyExistException">Like already exist</exception>
    public void SetLikes(LikesDto dto)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        var publication =
            PublicationsList.FirstOrDefault(x => x.ItemId == dto.PublicationId && x.OwnerId == dto.OwnerId);
        if (publication == null) throw new PublicationNotFoundException();

        var nodes = ReportElementsList.Cast<LikeReportElement>();
        foreach (var node in nodes)
        {
            var info = dto.SuccessLoaded
                ? new LikeInfo(publication.Id, dto.Likes!.Any(x => x == node.VkId))
                : new LikeInfo(publication.Id, false, false);
            node.AddLike(info);
        }

        Process++;
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    public void Finish(string? error = null)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);

        foreach (var element in ReportElementsList.Cast<LikeReportElement>())
        {
            if (string.IsNullOrEmpty(error) && element.Likes.Count != PublicationsList.Count)
                throw new ReportNotCompletedException(Id);
            var count = element.Likes.Where(x => x.IsLiked).DistinctBy(x => x.PublicationId).Count();
            if (count == PublicationsList.Count) element.Accept();
        }

        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);
    }
}