using System.Reflection;
using VkQ.Domain.Participants.Entities;
using VkQ.Infrastructure.DataStorage.Mappers.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;

internal class ParticipantMapper : IAggregateMapper<Participant, ParticipantModel>
{
    private static readonly FieldInfo ParticipantId =
        typeof(Participant).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;

    public Participant Map(ParticipantModel model)
    {
        var participant = new Participant(model.UserId, model.Name, model.VkId, model.Type);
        ParticipantId.SetValue(participant, model.Id);
        return participant;
    }
}