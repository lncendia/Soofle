using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.ParticipantReport.ValueObjects;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.ParticipantReport;

public class ParticipantReport : Report
{
    public ParticipantReport(User user) : base(user)
    {
    }

    private readonly List<ParticipantReportElement> _participants = new();
    public List<ParticipantReportElement> Participants => _participants.ToList();

    public void AddParticipant(string name, Guid participantId, bool isLeave)
    {
        if (_participants.Any(x => x.ParticipantId == participantId))
            throw new ReportElementAlreadyExistException(participantId);
        _participants.Add(new ParticipantReportElement(name, participantId, isLeave));
    }
}