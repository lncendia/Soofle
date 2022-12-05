using VkQ.Application.Abstractions.ReportsProcessors.DTOs;
using VkQ.Application.Abstractions.ReportsProcessors.Exceptions;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Application.Services.Services.ReportsProcessors.StaticMethods;

internal static class RequestInfoBuilder
{
    public static async Task<RequestInfo> GetInfoAsync(Report report, IUnitOfWork unitOfWork)
    {
        var user = await unitOfWork.UserRepository.Value.GetAsync(report.UserId);
        if (user?.Vk?.IsActive() ?? true) throw new VkIsNotActiveException();
        if (!user.Vk.ProxyId.HasValue) throw new ProxyIsNotSetException();
        var proxy = await unitOfWork.ProxyRepository.Value.GetAsync(user.Vk.ProxyId.Value);
        return new RequestInfo(user.Vk.AccessToken!, proxy!.Host, proxy.Port, proxy.Login, proxy.Password);
    }
}