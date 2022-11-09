namespace VkQ.Domain.Users.ValueObjects;

public class Subscription
{
    public Subscription(DateTimeOffset expirationDate)
    {
        ExpirationDate = expirationDate;
        SubscriptionDate = DateTimeOffset.Now;
    }

    public DateTimeOffset SubscriptionDate { get; }
    public DateTimeOffset ExpirationDate { get; }

    public bool IsExpired => ExpirationDate < DateTimeOffset.Now;
}