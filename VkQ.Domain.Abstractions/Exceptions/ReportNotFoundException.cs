namespace VkQ.Domain.Abstractions.Exceptions;

public class ReportNotFoundException : Exception
{
    public ReportNotFoundException():base("Report not found")
    {
    }
}