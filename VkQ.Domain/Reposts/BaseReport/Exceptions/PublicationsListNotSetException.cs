namespace VkQ.Domain.Reposts.BaseReport.Exceptions;

public class PublicationsListNotSetException : Exception
{
    public PublicationsListNotSetException() : base("Publications list not set")
    {
    }
}