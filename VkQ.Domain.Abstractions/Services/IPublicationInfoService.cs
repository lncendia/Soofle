using VkQ.Domain.Abstractions.DTOs;

namespace VkQ.Domain.Abstractions.Services;

public interface IPublicationInfoService
{
    Task<List<LikesDto>> GetLikesAsync(RequestInfo data, List<(long id, long owner)> publicationIds, CancellationToken token);
    Task<List<CommentsDto>> GetCommentsAsync(RequestInfo data, List<(long id, long owner)> publicationIds, CancellationToken token);
    Task<List<RepostsDto>> GetRepostsAsync(RequestInfo data, List<(long id, long owner)> publicationIds, CancellationToken token);
}