using VkNet.Exception;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

namespace Soofle.Infrastructure.VkRequests.Services;

public class RepostInfoService : IRepostInfoService
{
    private readonly ICaptchaSolver _solver;

    public RepostInfoService(ICaptchaSolver solver) => _solver = solver;

    public async Task<List<long>> GetAsync(RequestInfo data, long id, long owner, int count)
    {
        var api = await VkApi.BuildApiAsync(data, _solver);
        var task = api.Wall.GetRepostsAsync(owner, id, 0, count);
        try
        {
            var result = await task;
            return result.Profiles.Select(x => x.Id).Union(result.Groups.Select(x => x.Id)).ToList();
        }
        catch (VkApiException ex)
        {
            throw VkApi.GetException(ex);
        }
    }
}