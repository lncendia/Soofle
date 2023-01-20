using Soofle.Application.Abstractions.Profile.DTOs;

namespace Soofle.Application.Abstractions.Profile.ServicesInterfaces;

public interface IProfileService
{
    Task<ProfileDto> GetAsync(Guid userId);
}