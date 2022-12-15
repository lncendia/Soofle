using VkQ.Application.Abstractions.Participants.DTOs;
using VkQ.Domain.Participants.Entities;

namespace VkQ.Application.Abstractions.Participants.ServicesInterfaces;

public interface IParticipantMapper
{
    List<ParticipantDto> Map(List<Participant> participant);
}