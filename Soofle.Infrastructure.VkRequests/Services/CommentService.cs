using VkNet.Abstractions;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

namespace Soofle.Infrastructure.VkRequests.Services;

public class CommentInfoService : ICommentInfoService
{
    private readonly ICaptchaSolver _solver;

    public CommentInfoService(ICaptchaSolver solver) => _solver = solver;

    public async Task<List<(long id, string text)>> GetAsync(RequestInfo data, long id, long owner, int count)
    {
        var api = await VkApi.BuildApiAsync(data, _solver);
        var task = GetCommentsOfPostAsync(new List<Comment>(), api, id, owner, count);
        try
        {
            var result = await task;
            return result.Select(x => (x.FromId!.Value, x.Text)).ToList();
        }
        catch (VkApiException ex)
        {
            throw VkApi.GetException(ex);
        }
    }

    private static async Task<List<Comment>> GetCommentsOfPostAsync(List<Comment> comments, IVkApiCategories api,
        long id, long ownerId, int count, long? startId = null)
    {
        if (comments.Count >= count) return comments;
        var commentsInfo = await api.Wall.GetCommentsAsync(new WallGetCommentsParams
            { PostId = id, OwnerId = ownerId, Count = 100, PreviewLength = 10, StartCommentId = startId });
        comments.AddRange(commentsInfo.Items);
        if (commentsInfo.Count <= comments.Count || !commentsInfo.Items.Any()) return comments;
        await GetCommentsOfPostAsync(comments, api, id, ownerId, count, comments.Last().Id);

        return comments;
    }
}