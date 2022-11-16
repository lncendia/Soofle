namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ReportNotCompletedException : Exception
{
    public ReportNotCompletedException() : base("Report not completed")
    {
    }
}