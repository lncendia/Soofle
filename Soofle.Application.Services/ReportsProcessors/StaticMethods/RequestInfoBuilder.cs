using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.BaseReport.Entities;

namespace Soofle.Application.Services.ReportsProcessors.StaticMethods;

internal static class RequestInfoBuilder
{
    public static async Task<RequestInfo> GetInfoAsync(Report report, IUnitOfWork unitOfWork)
    {
        var user = await unitOfWork.UserRepository.Value.GetAsync(report.UserId);
        if (user == null) throw new UserNotFoundException();
        if (!user.HasVk) throw new VkIsNotActiveException();
        if (!user.ProxyId.HasValue) throw new ProxyIsNotSetException();
        var proxy = await unitOfWork.ProxyRepository.Value.GetAsync(user.ProxyId.Value);
        return new RequestInfo(user.Vk!.AccessToken, proxy!.Host, proxy.Port, proxy.Login, proxy.Password);
    }
}