namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class PublicationNotFoundException : Exception
{
    public PublicationNotFoundException():base("Report don't have publication with this id")
    {
    }
}