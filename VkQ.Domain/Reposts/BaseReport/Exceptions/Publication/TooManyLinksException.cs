namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class TooManyLinksException : Exception
{
    public TooManyLinksException() : base("Too many links for report. You cannot create more than 3 links")
    {
    }
}