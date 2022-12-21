using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Vk.DTOs;
using VkQ.Application.Abstractions.Vk.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Users.Entities;

namespace VkQ.Application.Services.Vk;

public class VkManager : IVkManager
{
    private readonly IVkLoginService _vkLoginService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProxyGetter _proxyGetter;

    public VkManager(IVkLoginService vkLoginService, IUnitOfWork unitOfWork, IProxyGetter proxy)
    {
        _vkLoginService = vkLoginService;
        _unitOfWork = unitOfWork;
        _proxyGetter = proxy;
    }

    public async Task SetVkAsync(Guid userId, string username, string password)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var token = user.Vk?.AccessToken;
        var proxy = await GetProxyAsync(user);
        user.SetVk(username, password);
        user.SetVkProxy(proxy);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        var tasks = new List<Task> { _unitOfWork.SaveChangesAsync() };
        if (!string.IsNullOrEmpty(token))
        {
            var t = _vkLoginService.DeactivateAsync(new VkLogoutDto(token, new VkProxyDto(proxy.Host,
                proxy.Port, proxy.Login, proxy.Password)));
            tasks.Add(t);
        }

        await Task.WhenAll(tasks);
    }

    public async Task ActivateVkAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var proxy = await GetProxyAsync(user);
        user.SetVkProxy(proxy);
        var token = await _vkLoginService.ActivateAsync(new VkLoginDto(user.Vk!.Login, user.Vk.Password,
            new VkProxyDto(proxy.Host, proxy.Port, proxy.Login, proxy.Password)));
        user.ActivateVk(token);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ActivateTwoFactorAsync(Guid userId, string code)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var proxy = await GetProxyAsync(user);
        user.SetVkProxy(proxy);
        var token = await _vkLoginService.ActivateTwoFactorAsync(new VkLoginDto(user.Vk!.Login, user.Vk.Password,
            new VkProxyDto(proxy.Host, proxy.Port, proxy.Login, proxy.Password)), code);
        user.ActivateVk(token);
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }


    private async Task<Domain.Proxies.Entities.Proxy> GetProxyAsync(User user)
    {
        if (user.Vk?.ProxyId != null)
        {
            var proxy = await _unitOfWork.ProxyRepository.Value.GetAsync(user.Vk.ProxyId.Value);
            if (proxy != null) return proxy;
        }

        var newProxy = await _proxyGetter.GetAsync();
        return newProxy;
    }
}