namespace VkQ.Domain.Abstractions.Exceptions;

public class VkRequestException : Exception
{
    public int Code { get; }

    public VkRequestException(int code, string? message, Exception? exception) : base(message, exception)
    {
        Code = code;
    }
}