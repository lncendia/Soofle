using Microsoft.EntityFrameworkCore;
using VkQ.Domain.Users.Entities;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.ModelMappers;

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
        if (model.Vk == null) user.Vk = null;
        else
        {
            user.Vk = new VkModel
            {
                Id = model.Vk.Id,
                AccessToken = model.Vk.AccessToken,
                Password = model.Vk.Password,
                ProxyId = model.Vk.ProxyId,
                Username = model.Vk.Login
            };
        }

        return user;
    }
}