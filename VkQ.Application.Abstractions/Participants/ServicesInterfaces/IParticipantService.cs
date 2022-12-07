using VkQ.Application.Abstractions.Participants.DTOs;
using VkQ.Domain.Participants.Enums;

namespace VkQ.Application.Abstractions.Participants.ServicesInterfaces;

public interface IParticipantService
{
    public Task<List<ParticipantsDto>> GetParticipantsAsync(Guid userId, int page, string? name, ParticipantType? type, bool? vip);
    public Task EditParticipantAsync(Guid userId, Guid participantId, string? note, bool? vip);
    public Task DeleteParticipantAsync(Guid userId, Guid participantId);
    public Task<ParticipantsDto> GetParticipantAsync(Guid userId, Guid participantId);

}