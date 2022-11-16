namespace VkQ.Domain.Abstractions.Exceptions;

public sealed class VkIsNotActiveException : Exception
{
    public VkIsNotActiveException() : base($"Vk is not active.")
    {
    }
}