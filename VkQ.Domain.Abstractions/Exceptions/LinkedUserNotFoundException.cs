namespace VkQ.Domain.Abstractions.Exceptions;

public class LinkedUserNotFoundException : Exception
{
    public LinkedUserNotFoundException() : base("Linked user not found")
    {
    }
}