using Soofle.Application.Abstractions.Users.DTOs;
using Soofle.Application.Abstractions.Users.Entities;

namespace Soofle.Application.Abstractions.Users.ServicesInterfaces;

public interface IUsersManager
{
    Task<List<UserShortDto>> FindAsync(SearchQuery query);
    Task<UserDto> GetAsync(Guid userId);
    Task<UserData> GetAuthenticationDataAsync(Guid userId);
    Task EditAsync(EditUserDto editData);
    Task ChangePasswordAsync(string email, string password);
    Task AddSubscribeAsync(Guid userId, TimeSpan timeSpan);
}