namespace VkQ.Application.Abstractions.Reports.Exceptions;

public class ReportNotFoundException : Exception
{
    public ReportNotFoundException() : base("Report not found")
    {
    }
}