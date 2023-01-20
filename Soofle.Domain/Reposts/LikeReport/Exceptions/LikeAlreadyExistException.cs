namespace Soofle.Domain.Reposts.LikeReport.Exceptions;

public class LikeAlreadyExistException : Exception
{
    public LikeAlreadyExistException():base("Like already exist")
    {
    }
}