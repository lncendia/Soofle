using Soofle.Application.Abstractions.VkRequests.DTOs;

namespace Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

public interface IRepostInfoService
{
    Task<List<long>> GetAsync(RequestInfo data, long id, long owner, int count);
}