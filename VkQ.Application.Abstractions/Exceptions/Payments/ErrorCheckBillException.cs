namespace VkQ.Application.Abstractions.Exceptions.Payments;

public class ErrorCheckBillException : Exception
{
    public ErrorCheckBillException(string message, Exception? exception) : base(
        $"Error when checking the bill: {message}.", exception)
    {
    }
}