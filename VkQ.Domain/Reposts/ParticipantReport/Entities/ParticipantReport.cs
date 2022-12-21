using VkQ.Domain.Participants.Entities;
using VkQ.Domain.ReportLogs.Enums;
using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.BaseReport.Events;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.ParticipantReport.DTOs;
using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.Domain.Reposts.ParticipantReport.Events;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReport : Report
{
    public ParticipantReport(User user, long vkId) : base(user)
    {
        VkId = vkId;
        AddDomainEvent(new ReportCreatedEvent(new[] { UserId }, Id, ReportType.Participants, CreationDate, "-"));
    }

    public long VkId { get; }

    public List<ParticipantReportElement> Participants => ReportElementsList.Cast<ParticipantReportElement>().ToList();

    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ReportNotStartedException">Report not initialized</exception>
    public void ProcessParticipantInfo(ParticipantDto participant)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        var node = ReportElementsList.Cast<ParticipantReportElement>().FirstOrDefault(x => x.VkId == participant.VkId);
        if (node != null)
        {
            if (node.Name != participant.Name) node.SetType(ElementType.Rename, participant.Name);
            else node.SetType(ElementType.Stay);
        }
        else
        {
            var item = new ParticipantReportElement(participant.Name, participant.VkId, null, participant.Type, null);
            item.SetType(ElementType.New);
            ReportElementsList.Add(item);
        }
    }


    /// <exception cref="ReportAlreadyStartedException">Report already started</exception>
    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ParticipantNotLinkedToReportException"></exception>
    public void Start(IList<Participant> participants)
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);

        var p = participants.FirstOrDefault(x => x.UserId != UserId);
        if (p != null) throw new ParticipantNotLinkedToReportException(p.Id);
        var grouperElements = participants.GroupBy(x => x.ParentParticipantId).ToList();

        foreach (var participant in grouperElements.First(x => x.Key == null))
        {
            var item = new ParticipantReportElement(participant.Name, participant.VkId, participant.Id,
                participant.Type, null);
            ReportElementsList.Add(item);
            var children = grouperElements.FirstOrDefault(x => x.Key == participant.Id);
            if (children == null) continue;
            ReportElementsList.AddRange(children.Select(x =>
                new ParticipantReportElement(x.Name, x.VkId, x.Id, x.Type, item)));
        }

        base.Start();
    }

    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ReportNotStartedException">Report not initialized</exception>
    public void Finish(string? error = null)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);

        foreach (var element in ReportElementsList.Cast<ParticipantReportElement>().Where(x => x.Parent == null))
        {
            var children = ReportElementsList.Cast<ParticipantReportElement>().Where(x => x.Parent == element);
            bool allChildStay = true;
            foreach (var participantReportElement in children)
            {
                switch (participantReportElement.Type)
                {
                    case null:
                        participantReportElement.SetType(ElementType.Leave);
                        break;
                    case ElementType.Stay:
                        ReportElementsList.Remove(participantReportElement);
                        break;
                }

                allChildStay = allChildStay && participantReportElement.Type == ElementType.Stay;
            }

            if (!element.Type.HasValue) element.SetType(ElementType.Leave);
            else if (element.Type == ElementType.Stay && allChildStay)
                ReportElementsList.Remove(element); //todo: check for update collection
        }

        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);

        var participants = ReportElementsList.Cast<ParticipantReportElement>().Select(x =>
            new ParticipantReportFinishedEvent.ParticipantDto(x.Id, x.NewName ?? x.Name, x.VkId, x.ParticipantType,
                x.Type!.Value));

        AddDomainEvent(new ParticipantReportFinishedEvent(Id, UserId, participants));
    }
}