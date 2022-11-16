namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class ElementsListEmptyException : Exception
{
    public ElementsListEmptyException() : base("Elements list is empty")
    {
    }
}