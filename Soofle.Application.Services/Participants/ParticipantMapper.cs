using Soofle.Application.Abstractions.Participants.DTOs;
using Soofle.Application.Abstractions.Participants.ServicesInterfaces;
using Soofle.Domain.Participants.Entities;

namespace Soofle.Application.Services.Participants;

public class ParticipantMapper : IParticipantMapper
{
    private static ParticipantDto Map(Participant participant, IEnumerable<ParticipantDto>? children) =>
        new(participant.Id, participant.Name, participant.Notes, participant.Type, participant.VkId, participant.Vip,
            children);

    public List<ParticipantDto> Map(List<Participant> participants)
    {
        var parentParticipants = participants.Where(x => !x.ParentParticipantId.HasValue).ToList();
        var childParticipants = participants.Where(x => x.ParentParticipantId.HasValue)
            .GroupBy(x => x.ParentParticipantId).ToList();
        return parentParticipants.Select(x =>
            Map(x, childParticipants.FirstOrDefault(y => y.Key == x.Id)?.Select(z => Map(z, null)).ToList())).ToList();
    }
}