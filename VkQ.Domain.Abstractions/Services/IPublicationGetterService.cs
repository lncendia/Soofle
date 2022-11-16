using VkQ.Domain.Abstractions.DTOs;

namespace VkQ.Domain.Abstractions.Services;

public interface IPublicationGetterService
{
    Task<List<PublicationDto>> GetPublicationsAsync(RequestInfo data, string hashtag, int count,
        DateTimeOffset? limitTime, CancellationToken token);
}