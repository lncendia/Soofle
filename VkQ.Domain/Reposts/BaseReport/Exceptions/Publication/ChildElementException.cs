namespace VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;

public class ChildElementException : Exception
{
    public ChildElementException() : base("Child element can't have child elements")
    {
    }
}