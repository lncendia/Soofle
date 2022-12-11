using System.Net.Mail;
using System.Text.RegularExpressions;
using VkQ.Domain.Abstractions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Users.Exceptions;
using VkQ.Domain.Users.ValueObjects;

namespace VkQ.Domain.Users.Entities;

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
        Subscription = new Subscription(offset.Add(timeSpan));
    }

    public bool IsSubscribed => Subscription is { IsExpired: false };

    /// <exception cref="VkIsNotSetException"></exception>
    public void SetVkProxy(Proxy proxy)
    {
        if (Vk == null) throw new VkIsNotSetException();
        Vk.SetProxy(proxy);
    }

    public void SetVk(string username, string password)
    {
        if (Vk == null) Vk = new Vk(username, password);
        else Vk.UpdateData(username, password);
    }

    public bool HasVk => Vk != null;

    /// <exception cref="VkIsNotSetException"></exception>
    public void ActivateVk(string token)
    {
        if (Vk == null) throw new VkIsNotSetException();
        Vk.UpdateToken(token);
    }
}