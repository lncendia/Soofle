namespace VkQ.Domain.Reposts.BaseReport.Exceptions;

public class ReportElementAlreadyExistException : Exception
{
    public ReportElementAlreadyExistException(Guid participantId) : base(
        $"Participant with id {participantId} already exist in report")
    {
    }
}