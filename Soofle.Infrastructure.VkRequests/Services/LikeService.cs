using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Model.RequestParams;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

namespace Soofle.Infrastructure.VkRequests.Services;

public class LikeInfoService : ILikeInfoService
{
    private readonly ICaptchaSolver _solver;

    public LikeInfoService(ICaptchaSolver solver) => _solver = solver;

    public async Task<List<long>> GetAsync(RequestInfo data, long id, long owner, int count)
    {
        var api = await VkApi.BuildApiAsync(data, _solver);
        var task = api.Likes.GetListAsync(new LikesGetListParams
            { Type = LikeObjectType.Post, Count = (uint)count, ItemId = id, OwnerId = owner });
        try
        {
            var result = await task;
            return result.ToList();
        }
        catch (VkApiException ex)
        {
            throw VkApi.GetException(ex);
        }
    }
}