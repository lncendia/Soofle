namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ReportAlreadyStartedException : Exception
{
    public ReportAlreadyStartedException(Guid reportId) : base("Report already started")
    {
        ReportId = reportId;
    }

    public Guid ReportId { get; }
}