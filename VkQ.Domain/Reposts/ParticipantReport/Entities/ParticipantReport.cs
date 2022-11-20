using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.ParticipantReport.Exceptions;
using VkQ.Domain.Reposts.ParticipantReport.ValueObjects;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReport : Report
{
    public ParticipantReport(User user) : base(user)
    {
    }

    private readonly List<ParticipantReportElement> _participants = new();
    public List<ParticipantReportElement> Participants => _participants.ToList();

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="ParticipantRecordAlreadyExistException">Record already exist</exception>
    public void AddParticipant(string name, long vkId, Guid participantId, bool isLeave)
    {
        if (!IsStarted) throw new ReportNotStartedException();
        if (IsCompleted) throw new ReportAlreadyCompletedException();
        if (_participants.Any(x => x.ParticipantId == participantId))
            throw new ParticipantRecordAlreadyExistException();
        _participants.Add(new ParticipantReportElement(name, vkId, participantId, isLeave));
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    public void Finish(string? error = null)
    {
        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);
    }
}