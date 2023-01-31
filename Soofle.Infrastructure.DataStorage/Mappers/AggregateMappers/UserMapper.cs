using System.Reflection;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Mappers.StaticMethods;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.ValueObjects;

namespace Soofle.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class UserMapper : IAggregateMapperUnit<User, UserModel>
{
    private static readonly FieldInfo UserSubscription =
        typeof(User).GetField("<Subscription>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo UserProxy =
        typeof(User).GetField("<ProxyId>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    private static readonly FieldInfo TargetDate =
        typeof(Target).GetField("<SetDate>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public User Map(UserModel model)
    {
        var user = new User(model.Name, model.Email);
        IdFields.AggregateId.SetValue(user, model.Id);
        if (model.Target.HasValue)
        {
            user.SetTarget(model.Target.Value);
            TargetDate.SetValue(user.Target, model.TargetSetTime);
        }
        
        UserProxy.SetValue(user, model.ProxyId);
        if (model.SubscriptionDate.HasValue)
            UserSubscription.SetValue(user, GetSubscription(model.SubscriptionDate.Value, model.ExpirationDate!.Value));
        if (model.AccessToken != null) user.SetVk(model.VkName!, model.AccessToken);
        return user;
    }

    private static readonly Type SubscriptionElementType = typeof(Subscription);

    private static Subscription GetSubscription(DateTimeOffset start, DateTimeOffset end)
    {
        object?[] args = { end, start };
        return (Subscription)SubscriptionElementType.Assembly.CreateInstance(SubscriptionElementType.FullName!, false,
            BindingFlags.Instance | BindingFlags.NonPublic, null, args!, null, null)!;
    }
}