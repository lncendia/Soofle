namespace Soofle.Domain.Reposts.CommentReport.Exceptions;

public class CommentAlreadyExistException : Exception
{
    public CommentAlreadyExistException() : base("Comment already exist")
    {
    }
}