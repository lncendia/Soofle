using Soofle.Application.Abstractions.VkRequests.DTOs;

namespace Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

public interface IParticipantsService
{
    Task<List<ParticipantDto>> GetFriendsAsync(RequestInfo data, long id,int count, CancellationToken token);
    Task<List<ParticipantDto>> GetSubscriptionsAsync(RequestInfo data, long id, int count, CancellationToken token);
}