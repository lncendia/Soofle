using Soofle.Application.Abstractions.Links.DTOs;

namespace Soofle.Application.Abstractions.Links.ServicesInterfaces;

public interface ILinkManager
{
    Task<AddLinkDto> AddAsync(Guid user1, string email);
    Task DeleteAsync(Guid user, Guid linkId);
    Task AcceptAsync(Guid user, Guid linkId);
}