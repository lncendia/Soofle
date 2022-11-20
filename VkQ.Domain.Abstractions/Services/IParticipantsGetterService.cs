using VkQ.Domain.Abstractions.DTOs;

namespace VkQ.Domain.Abstractions.Services;

public interface IParticipantsGetterService
{
    Task<List<ParticipantDto>> GetParticipantsAsync(RequestInfo data, long id, CancellationToken token);
}