using VkQ.Application.Abstractions.Users.DTOs;

namespace VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;

public interface IProfileService
{
    Task<ProfileDto> GetAsync(Guid userId);
}