namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class PublicationsListEmptyException : Exception
{
    public PublicationsListEmptyException() : base("Publications list is empty")
    {
    }
}