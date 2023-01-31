using Microsoft.AspNetCore.Identity;

namespace Soofle.Application.Abstractions.Users.ServicesInterfaces;

public interface IVkManager
{
    Task SetAsync(Guid userId, ExternalLoginInfo info);
}