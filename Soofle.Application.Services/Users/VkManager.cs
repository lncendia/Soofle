using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.Users.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;

namespace Soofle.Application.Services.Users;

public class VkManager : IVkManager
{
    private readonly IUnitOfWork _unitOfWork;

    public VkManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SetAsync(Guid userId, ExternalLoginInfo info)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        user.SetVk(info.Principal.FindFirstValue(ClaimTypes.GivenName) + ' ' +
                   info.Principal.FindFirstValue(ClaimTypes.Surname),
            info.AuthenticationTokens.First(x => x.Name == "access_token").Value);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}