namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ReportNotStartedException : Exception
{
    public ReportNotStartedException() : base("Report not started")
    {
    }
}