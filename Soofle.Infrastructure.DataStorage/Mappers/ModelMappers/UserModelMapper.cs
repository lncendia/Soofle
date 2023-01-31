using Microsoft.EntityFrameworkCore;
using Soofle.Infrastructure.DataStorage.Context;
using Soofle.Infrastructure.DataStorage.Mappers.Abstractions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Users.Entities;

namespace Soofle.Infrastructure.DataStorage.Mappers.ModelMappers;

internal class UserModelMapper : IModelMapperUnit<UserModel, User>
{
    private readonly ApplicationDbContext _context;

    public UserModelMapper(ApplicationDbContext context) => _context = context;

    public async Task<UserModel> MapAsync(User model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id) ?? new UserModel { Id = model.Id };
        user.Name = model.Name;
        user.Email = model.Email;
        user.SubscriptionDate = model.Subscription?.SubscriptionDate;
        user.ExpirationDate = model.Subscription?.ExpirationDate;
        user.Target = model.Target?.Id;
        user.TargetSetTime = model.Target?.SetDate;
        user.ProxyId = model.ProxyId;
        if (!model.HasVk) return user;
        user.VkName = model.Vk!.Name;
        user.AccessToken = model.Vk.AccessToken;
        return user;
    }
}