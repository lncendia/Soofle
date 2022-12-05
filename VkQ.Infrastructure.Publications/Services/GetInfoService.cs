using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkQ.Application.Abstractions.ReportsProcessors.DTOs;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using LikesDto = VkQ.Application.Abstractions.ReportsProcessors.DTOs.LikesDto;

namespace VkQ.Infrastructure.Publications.Services;

public class GetInfoService : IPublicationInfoService
{
    private readonly ICaptchaSolver _solver;

    public GetInfoService(ICaptchaSolver solver) => _solver = solver;

    public async Task<List<LikesDto>> GetLikesAsync(RequestInfo data, List<(long id, long owner)> publicationIds,
        CancellationToken token)
    {
        var info = new List<LikesDto>();
        var api = await VkApi.BuildApiAsync(data, _solver);
        for (var i = 0; i < publicationIds.Count; i += 3)
        {
            token.ThrowIfCancellationRequested();
            var pubs = publicationIds.Skip(i).Take(3).ToList();
            var likes = pubs.Select(x =>
                api.Likes.GetListAsync(new LikesGetListParams
                    { Type = LikeObjectType.Post, Count = 1000, ItemId = x.id, OwnerId = x.owner })).ToList();
            await Task.WhenAll(likes);
            //UserAuthorizationFailException
            for (int j = 0; j < pubs.Count; j++)
            {
                var like = likes[i].IsCompletedSuccessfully
                    ? new LikesDto(pubs[j].id, pubs[j].owner, likes[i].Result.ToList())
                    : new LikesDto(pubs[j].id, pubs[j].owner);
                info.Add(like);
            }

            await Task.Delay(500, token);
        }

        return info;
    }

    public async Task<List<CommentsDto>> GetCommentsAsync(RequestInfo data, List<(long id, long owner)> publicationIds,
        CancellationToken token)
    {
        var info = new List<CommentsDto>();
        var api = await VkApi.BuildApiAsync(data, _solver);
        for (var i = 0; i < publicationIds.Count; i += 3)
        {
            token.ThrowIfCancellationRequested();
            var pubs = publicationIds.Skip(i).Take(3).ToList();
            var comments = pubs.Select(x => GetCommentsOfPostAsync(new List<Comment>(), api, x.id, x.owner)).ToList();
            await Task.WhenAll(comments);
            for (int j = 0; j < pubs.Count; j++)
            {
                info.Add(new CommentsDto(pubs[j].id, pubs[j].owner,
                    comments[j].IsCompletedSuccessfully
                        ? comments[j].Result.Select(x => new CommentDto(x.OwnerId!.Value, x.Text)).ToList()
                        : new List<CommentDto>()));
            }

            await Task.Delay(500, token);
        }

        return info;
    }

    private static async Task<List<Comment>> GetCommentsOfPostAsync(List<Comment> comments, IVkApiCategories api,
        long id,
        long ownerId,
        long? startId = null)
    {
        if (comments.Count >= 500) return comments;
        var commentsInfo = await api.Wall.GetCommentsAsync(new WallGetCommentsParams
            { PostId = id, OwnerId = ownerId, Count = 100, PreviewLength = 15, StartCommentId = startId });
        comments.AddRange(commentsInfo.Items);
        if (commentsInfo.Count <= comments.Count || !commentsInfo.Items.Any()) return comments;
        await GetCommentsOfPostAsync(comments, api, id, ownerId, comments.Last().Id);

        return comments;
    }

    public async Task<List<RepostsDto>> GetRepostsAsync(RequestInfo data, List<(long id, long owner)> publicationIds,
        CancellationToken token)
    {
        var info = new List<RepostsDto>();
        var api = await VkApi.BuildApiAsync(data, _solver);
        for (var i = 0; i < publicationIds.Count; i += 3)
        {
            token.ThrowIfCancellationRequested();
            var pubs = publicationIds.Skip(i).Take(3).ToList();
            var reposts = pubs.Select(x =>
                api.Wall.GetRepostsAsync(x.owner, x.id, 1000, 0)).ToList();
            await Task.WhenAll(reposts);
            for (int j = 0; j < pubs.Count; j++)
            {
                info.Add(new RepostsDto(pubs[j].id, pubs[j].owner,
                    reposts[j].IsCompletedSuccessfully
                        ? reposts[j].Result.Profiles.Select(x => x.Id).Distinct().ToList()
                        : new List<long>()));
            }

            await Task.Delay(1000, token);
        }

        return info;
    }
}