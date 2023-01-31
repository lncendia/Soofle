using System.Net.Mail;
using System.Text.RegularExpressions;
using Soofle.Domain.Abstractions;
using Soofle.Domain.Proxies.Entities;
using Soofle.Domain.Users.Exceptions;
using Soofle.Domain.Users.ValueObjects;

namespace Soofle.Domain.Users.Entities;

public class User : AggregateRoot
{
    public User(string name, string email)
    {
        Name = name;
        _name = name;
        _email = email;
        Email = email;
    }

    public Vk? Vk { get; private set; }
    public Subscription? Subscription { get; private set; }
    public Target? Target { get; private set; }
    public Guid? ProxyId { get; private set; }

    private string _email;

    /// <exception cref="InvalidEmailException"></exception>
    public string Email
    {
        get => _email;
        set
        {
            try
            {
                _email = new MailAddress(value).Address;
            }
            catch (FormatException)
            {
                throw new InvalidEmailException(value);
            }
        }
    }


    private string _name;

    /// <exception cref="InvalidNicknameException"></exception>
    public string Name
    {
        get => _name;
        set
        {
            if (Regex.IsMatch(value, "^[a-zA-Zа-яА-Я0-9_ ]{3,20}$")) _name = value;
            else throw new InvalidNicknameException(value);
        }
    }

    /// <exception cref="InvalidOperationException"></exception>
    public void AddSubscription(TimeSpan timeSpan)
    {
        if (timeSpan.Ticks <= 0) throw new InvalidOperationException("Time span must be positive");
        var offset = Subscription is { IsExpired: false } ? Subscription.ExpirationDate : DateTimeOffset.Now;
        Subscription = new Subscription(offset.Add(timeSpan), Subscription?.SubscriptionDate);
    }

    public bool IsSubscribed => Subscription is { IsExpired: false };
    public void SetProxy(Proxy proxy) => ProxyId = proxy.Id;

    public void SetVk(string name, string token) => Vk = new Vk(name, token);

    public void SetTarget(long id)
    {
        var dateComparison = DateTimeOffset.Now.AddDays(-1);
        if (Target != null && Target.SetDate > dateComparison)
            throw new TargetChangeException(Target.SetDate - dateComparison);
        Target = new Target(id);
    }

    public bool HasVk => Vk != null;
}