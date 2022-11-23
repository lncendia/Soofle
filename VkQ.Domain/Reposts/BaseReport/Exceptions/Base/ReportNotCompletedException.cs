namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ReportNotCompletedException : Exception
{
    public ReportNotCompletedException(Guid reportId) : base("Report not completed")
    {
        ReportId = reportId;
    }
    public Guid ReportId { get; }
}