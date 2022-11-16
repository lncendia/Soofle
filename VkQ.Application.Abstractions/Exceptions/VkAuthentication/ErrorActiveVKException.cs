namespace VkQ.Application.Abstractions.Exceptions.VkAuthentication;

public class ErrorActiveVkException : Exception
{
    public ErrorActiveVkException(string message, Exception? innerEx = null) : base(
        $"Can't active VK - {message}.", innerEx)
    {
    }
}