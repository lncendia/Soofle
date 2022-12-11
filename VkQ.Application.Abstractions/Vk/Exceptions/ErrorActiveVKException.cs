namespace VkQ.Application.Abstractions.Vk.Exceptions;

public class ErrorActiveVkException : Exception
{
    public ErrorActiveVkException(string message, Exception? innerEx = null) : base(
        $"Can't active VK - {message}.", innerEx)
    {
    }
}