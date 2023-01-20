namespace Soofle.Application.Abstractions.ReportsProcessors.Exceptions;

public sealed class VkIsNotActiveException : Exception
{
    public VkIsNotActiveException() : base($"Vk is not active")
    {
    }
}