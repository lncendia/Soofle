using System.Reflection;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.ValueObjects;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class UserMapper : IAggregateMapper<User, UserModel>
{
    private static readonly Type UserType = typeof(User);

    private static readonly FieldInfo UserId =
        UserType.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo UserVk =
        UserType.GetField("<Vk>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo UserSubscription =
        UserType.GetField("<Subscription>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public User Map(UserModel model)
    {
        var user = new User(model.Name, model.Email);
        UserId.SetValue(user, model.Id);

        if (model.SubscriptionDate.HasValue)
            UserSubscription.SetValue(user, GetSubscription(model.SubscriptionDate.Value, model.ExpirationDate!.Value));

        if (model.Vk != null) UserVk.SetValue(user, GetVk(model.Vk));
        return user;
    }


    private static readonly Type VkType = typeof(Vk);

    private static readonly FieldInfo VkId =
        VkType.GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo VkProxy =
        VkType.GetField("<ProxyId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;


    private static Vk GetVk(VkModel model)
    {
        var vk = new Vk(model.Username, model.Password);
        VkId.SetValue(vk, model.Id);
        VkProxy.SetValue(vk, model.ProxyId);
        if (!string.IsNullOrEmpty(model.AccessToken)) vk.UpdateToken(model.AccessToken);
        return vk;
    }

    private static readonly FieldInfo SubscriptionEndDate =
        typeof(Subscription).GetField("<SubscriptionDate>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static Subscription GetSubscription(DateTimeOffset start, DateTimeOffset end)
    {
        var sub = new Subscription(end);
        SubscriptionEndDate.SetValue(sub, start);
        return sub;
    }
}