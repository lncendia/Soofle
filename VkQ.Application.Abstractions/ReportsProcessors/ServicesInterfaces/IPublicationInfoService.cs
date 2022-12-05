using VkQ.Application.Abstractions.ReportsProcessors.DTOs;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IPublicationInfoService
{
    Task<List<LikesDto>> GetLikesAsync(RequestInfo data, List<(long id, long owner)> publicationIds, CancellationToken token);
    Task<List<CommentsDto>> GetCommentsAsync(RequestInfo data, List<(long id, long owner)> publicationIds, CancellationToken token);
    Task<List<RepostsDto>> GetRepostsAsync(RequestInfo data, List<(long id, long owner)> publicationIds, CancellationToken token);
}