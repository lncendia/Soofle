using Soofle.Application.Abstractions.VkRequests.DTOs;

namespace Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

public interface ICommentInfoService
{
    Task<List<(long id, string text)>> GetAsync(RequestInfo data, long id, long owner, int count);
}