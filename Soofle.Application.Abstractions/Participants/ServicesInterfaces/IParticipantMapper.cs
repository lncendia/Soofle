using Soofle.Application.Abstractions.Participants.DTOs;
using Soofle.Domain.Participants.Entities;

namespace Soofle.Application.Abstractions.Participants.ServicesInterfaces;

public interface IParticipantMapper
{
    List<ParticipantDto> Map(List<Participant> participant);
}