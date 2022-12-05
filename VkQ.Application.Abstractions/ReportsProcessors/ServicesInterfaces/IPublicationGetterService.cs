using VkQ.Application.Abstractions.ReportsProcessors.DTOs;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IPublicationGetterService
{
    Task<List<PublicationDto>> GetPublicationsAsync(RequestInfo data, string hashtag, int count,
        DateTimeOffset? limitTime, CancellationToken token);
}