namespace VkQ.Application.Abstractions.Reports.Exceptions;

public class UserNotOwnerException : Exception
{
    public UserNotOwnerException() : base("User is not owner of this report")
    {
    }
}