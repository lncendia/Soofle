﻿using Soofle.Domain.Links.Entities;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.ReportLogs.Enums;
using Soofle.Domain.Reposts.BaseReport.Events;
using Soofle.Domain.Reposts.BaseReport.Exceptions;
using Soofle.Domain.Reposts.LikeReport.DTOs;
using Soofle.Domain.Reposts.LikeReport.Exceptions;
using Soofle.Domain.Reposts.LikeReport.ValueObjects;
using Soofle.Domain.Reposts.PublicationReport.DTOs;
using Soofle.Domain.Reposts.PublicationReport.Exceptions;
using Soofle.Domain.Users.Entities;

namespace Soofle.Domain.Reposts.LikeReport.Entities;

public class LikeReport : PublicationReport.Entities.PublicationReport
{
    public LikeReport(User user, string hashtag, DateTimeOffset? startDate = null,
        IReadOnlyCollection<Link>? coAuthors = null) : base(user, hashtag, startDate, coAuthors)
    {
        AddDomainEvent(new ReportCreatedEvent(LinkedUsers.Concat(new[] { UserId }), Id, ReportType.Likes, CreationDate,
            Hashtag));
    }

    public IReadOnlyCollection<LikeReportElement> Elements => ReportElementsList.Cast<LikeReportElement>().ToList();

    ///<exception cref="ReportAlreadyStartedException">Report already started</exception>
    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ParticipantNotLinkedToReportException"></exception>
    public void Start(IEnumerable<ChatParticipants> participants, IEnumerable<PublicationDto> publications)
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

        var groupedElements = participants.GroupBy(x => x.ParentParticipantId).ToList();
        if (!groupedElements.Any()) return;
        var id = 1;
        foreach (var participant in groupedElements.First(x => x.Key == null))
        {
            var item = new LikeReportElement(participant.Name, likeChatName, participant.VkId, participant.Id,
                participant.Vip, participant.Notes, null, id++);
            elements.Add(item);
            var children = groupedElements.FirstOrDefault(x => x.Key == participant.Id);
            if (children == null) continue;
            elements.AddRange(children.Select(x =>
                new LikeReportElement(x.Name, likeChatName, x.VkId, x.Id, x.Vip, x.Notes, item, id++)));
        }
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="PublicationNotFoundException">Publication not found</exception>
    ///<exception cref="LikeAlreadyExistException">Like already exist</exception>
    public void SetLikes(LikesDto likes)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        var publication =
            PublicationsList.FirstOrDefault(x => x.ItemId == likes.PublicationId && x.OwnerId == likes.OwnerId);
        if (publication == null) throw new PublicationNotFoundException();
        publication.IsLoaded = likes.SuccessLoaded;
        var nodes = ReportElementsList.Cast<LikeReportElement>();
        foreach (var node in nodes)
        {
            var isConfirmed = likes.SuccessLoaded &&
                              (publication.OwnerId == node.VkId || likes.Likes!.Any(x => x == node.VkId));
            var info = new LikeInfo(publication.Id, isConfirmed);
            node.AddLike(info);
        }

        Process++;
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    public void Finish(string? error = null)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        foreach (var element in ReportElementsList.Cast<LikeReportElement>())
        {
            if (string.IsNullOrEmpty(error) && element.Likes.Count != PublicationsList.Count)
                throw new ReportNotCompletedException(Id);
            var count = element.Likes.Where(x => x.IsConfirmed).DistinctBy(x => x.PublicationId).Count();
            if (count == PublicationsList.Count) element.Accept();
        }

        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);
    }
}