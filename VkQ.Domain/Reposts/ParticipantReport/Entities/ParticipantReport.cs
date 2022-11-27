using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.ParticipantReport.DTOs;
using VkQ.Domain.Reposts.ParticipantReport.Exceptions;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Reposts.ParticipantReport.Entities;

public class ParticipantReport : Report
{
    public ParticipantReport(User user, long vkId) : base(user)
    {
        VkId = vkId;
    }

    public long VkId { get; }

    public List<ParticipantReportElement> Participants => ReportElementsList.Cast<ParticipantReportElement>().ToList();

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="ParticipantRecordAlreadyExistException">Record already exist</exception>
    public void AddParticipants(IEnumerable<AddParticipantDto> dtos)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        var addParticipantDtos = dtos.ToList();
        if (ReportElementsList.Any())
        {
            var participants =
                ReportElementsList.Concat(ReportElementsList.Cast<ParticipantReportElement>().SelectMany(x => x.Children)).Select(x => x.VkId); //All elements

            var intersectFirst = addParticipantDtos.Select(x => x.VkId).Intersect(participants).FirstOrDefault();
            if (intersectFirst != default) throw new ParticipantRecordAlreadyExistException(intersectFirst);
        }

        ReportElementsList.AddRange(addParticipantDtos.Select(GetElement));
    }

    private static ParticipantReportElement GetElement(AddParticipantDto dto) =>
        new(dto.Name, dto.OldName, dto.VkId, dto.Type, dto.Children?.Select(GetElement));


    ///<exception cref="ReportAlreadyStartedException">Report already started</exception>
    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    public new void Start()
    {
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);
        if (IsStarted) throw new ReportAlreadyStartedException(Id);
        base.Start();
    }

    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    public void Finish(string? error = null)
    {
        if (!IsStarted) throw new ReportNotStartedException(Id);
        if (IsCompleted) throw new ReportAlreadyCompletedException(Id);

        if (string.IsNullOrEmpty(error))
            Succeed();
        else
            Fail(error);
    }
}