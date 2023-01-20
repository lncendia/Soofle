namespace Soofle.Application.Abstractions.Vk.ServicesInterfaces;

public interface IVkManager
{
    public Task SetAsync(Guid userId, string username, string password);
    public Task ActivateAsync(Guid userId);
    public Task ActivateTwoFactorAsync(Guid userId, string code);
}