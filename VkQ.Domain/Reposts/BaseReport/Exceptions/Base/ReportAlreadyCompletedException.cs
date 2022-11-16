namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

public class ReportAlreadyCompletedException : Exception
{
    public ReportAlreadyCompletedException() : base("Report already completed")
    {
    }
}