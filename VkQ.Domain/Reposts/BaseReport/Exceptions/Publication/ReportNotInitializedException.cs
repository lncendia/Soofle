namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class ReportNotInitializedException : Exception
{
    public ReportNotInitializedException() : base("Report not started")
    {
    }
}