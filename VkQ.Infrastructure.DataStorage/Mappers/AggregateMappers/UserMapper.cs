using System.Reflection;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.ValueObjects;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Mappers.StaticMethods;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class UserMapper : IAggregateMapperUnit<User, UserModel>
{

    private static readonly FieldInfo UserSubscription =
        typeof(User).GetField("<Subscription>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public User Map(UserModel model)
    {
        var user = new User(model.Name, model.Email);
        IdFields.AggregateId.SetValue(user, model.Id);

        if (model.SubscriptionDate.HasValue)
            UserSubscription.SetValue(user, GetSubscription(model.SubscriptionDate.Value, model.ExpirationDate!.Value));

        if (model.Vk == null) return user;
        user.SetVk(model.Vk.Username, model.Vk.Password);
        IdFields.EntityId.SetValue(user.Vk, model.Id);
        VkProxy.SetValue(user.Vk, model.Vk.ProxyId);
        if (!string.IsNullOrEmpty(model.Vk.AccessToken)) user.ActivateVk(model.Vk.AccessToken);

        return user;
    }


    private static readonly FieldInfo VkProxy =
        typeof(Vk).GetField("<ProxyId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

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