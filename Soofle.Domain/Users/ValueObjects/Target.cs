namespace Soofle.Domain.Users.ValueObjects;

public class Target
{
    internal Target(long id)
    {
        Id = id;
        SetDate = DateTimeOffset.Now;
    }

    public long Id { get; }
    public DateTimeOffset SetDate { get; }
}