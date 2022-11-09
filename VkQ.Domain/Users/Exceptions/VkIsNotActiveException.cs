using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Users.Exceptions;

public sealed class VkNotActiveException : Exception
{
    public VkNotActiveException(Vk vk) : base($"Vk {vk.Id} is not active.")
    {
    }
}