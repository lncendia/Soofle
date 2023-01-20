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
        var user = await _context.Users.Include(x => x.Vk).FirstOrDefaultAsync(x => x.Id == model.Id) ??
                   new UserModel { Id = model.Id };
        user.Name = model.Name;
        user.Email = model.Email;
        user.SubscriptionDate = model.Subscription?.SubscriptionDate;
        user.ExpirationDate = model.Subscription?.ExpirationDate;
        user.ChatId = model.ChatId;
        await MapVkAsync(model, user);

        return user;
    }

    private async Task MapVkAsync(User user, UserModel model)
    {
        if (user.Vk == null)
        {
            if (model.Vk != null) _context.Remove(model.Vk);
        }
        else
        {
            if (model.Vk == null)
            {
                model.Vk = Create(user.Vk);
                await _context.AddAsync(model.Vk);
            }
            else
            {
                model.Vk.EntityId = user.Vk.Id;
                model.Vk.Username = user.Vk.Login;
                model.Vk.Password = user.Vk.Password;
                model.Vk.AccessToken = user.Vk.AccessToken;
                model.Vk.ProxyId = user.Vk.ProxyId;
            }
        }
    }

    private static VkModel Create(Vk vk)
    {
        return new VkModel
        {
            EntityId = vk.Id,
            AccessToken = vk.AccessToken,
            Password = vk.Password,
            ProxyId = vk.ProxyId,
            Username = vk.Login
        };
    }
}