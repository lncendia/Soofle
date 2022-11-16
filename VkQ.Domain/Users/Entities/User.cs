using System.Net.Mail;
using System.Text.RegularExpressions;
using VkQ.Domain.Proxies.Entities;
using VkQ.Domain.Users.Exceptions;
using VkQ.Domain.Users.ValueObjects;

namespace VkQ.Domain.Users.Entities;

public class User
{
    public User(string name, string email)
    {
        Name = name;
        _name = name;
        _email = email;
        Email = email;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public Vk? Vk { get; private set; }
    public Subscription? Subscription { get; private set; }

    private string _email;

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

    public string Name
    {
        get => _name;
        set
        {
            if (Regex.IsMatch(value, "^[a-zA-Zа-яА-Я0-9_ ]{3,20}$")) _name = value;
            else throw new InvalidNicknameException(value);
        }
    }

    public void AddSubscription(int days)
    {
        var offset = Subscription is { IsExpired: false } ? Subscription.ExpirationDate : DateTimeOffset.Now;
        Subscription = new Subscription(offset.AddDays(days));
    }

    public bool IsSubscribed => Subscription is { IsExpired: false };

    public void SetVkProxy(Proxy proxy)
    {
        if (Vk == null) throw new VkIsNotSetException();
        Vk.SetProxy(proxy);
    }

    public void SetVk(string username, string password)
    {
        Vk = new Vk(1, username, password);
    }

    public void ActivateVk(string token)
    {
        if (Vk == null) throw new VkIsNotSetException();
        Vk.UpdateToken(token);
    }
}