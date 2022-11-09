namespace VkQ.Domain.Reposts.BaseReport.Exceptions;

public class ChildElementException : Exception
{
    public ChildElementException() : base("Child element can't have child elements")
    {
    }
}