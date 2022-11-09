using VkQ.Domain.Abstractions.DTOs;

namespace VkQ.Domain.Abstractions.Services;

public interface IPublicationGetterService
{
    Task<List<PublicationDto>> GetPublicationsAsync(RequestInfo data, string hashtag, int count,
        DateTimeOffset? dateFrom);

    Task<List<LikesDto>> GetLikesAsync(RequestInfo data, List<long> publicationIds);
    Task<List<CommentsDto>> GetCommentsAsync(RequestInfo data, List<long> publicationIds);
    Task<List<RepostsDto>> GetRepostsAsync(RequestInfo data, List<long> publicationIds);
}