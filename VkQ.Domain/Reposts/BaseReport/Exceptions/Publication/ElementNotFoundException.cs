namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class ElementNotFoundException : Exception
{
    public ElementNotFoundException() : base("Element not found")
    {
    }
}