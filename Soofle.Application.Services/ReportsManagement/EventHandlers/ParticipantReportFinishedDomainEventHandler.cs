using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification;
using Soofle.Domain.Reposts.ParticipantReport.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Events;

namespace Soofle.Application.Services.ReportsManagement.EventHandlers;

public class ParticipantReportFinishedDomainEventHandler : INotificationHandler<ParticipantReportFinishedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public ParticipantReportFinishedDomainEventHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _cache = memoryCache;
    }

    public async Task Handle(ParticipantReportFinishedEvent notification, CancellationToken cancellationToken)
    {
        if (!_cache.TryGetValue(CachingConstants.GetParticipantsKey(notification.UserId),
                out List<Participant>? participants))
        {
            participants =
                await _unitOfWork.ParticipantRepository.Value.FindAsync(
                    new ParticipantsByUserIdSpecification(notification.UserId));
        }
        else participants ??= new List<Participant>();


        foreach (var participant in notification.Participants)
        {
            if (participant.ElementType == ElementType.New)
            {
                var newParticipant = new Participant(notification.UserId, participant.Name, participant.VkId,
                    participant.Type, participants);
                participants.Add(newParticipant);
                await _unitOfWork.ParticipantRepository.Value.AddAsync(newParticipant);

                continue;
            }

            var participantToUpdate = participants.FirstOrDefault(x => x.Id == participant.Id);
            if (participantToUpdate == null) continue;
            switch (participant.ElementType)
            {
                case ElementType.Rename:
                    participantToUpdate.UpdateName(participant.Name);
                    await _unitOfWork.ParticipantRepository.Value.UpdateAsync(participantToUpdate);
                    break;
                case ElementType.Leave:
                    participants.Remove(participantToUpdate);
                    await _unitOfWork.ParticipantRepository.Value.DeleteAsync(participantToUpdate.Id);
                    break;
            }
        }

        _cache.Remove(CachingConstants.GetParticipantsKey(notification.UserId));
    }
}