using Soofle.Application.Abstractions.VkRequests.DTOs;

namespace Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

public interface IPublicationService
{
    Task<List<PublicationDto>> GetAsync(RequestInfo data, string hashtag, int count,
        DateTimeOffset? limitTime, CancellationToken token);
}