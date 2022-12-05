using MediatR;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.Domain.Reposts.ParticipantReport.Events;

namespace VkQ.Application.Abstractions.Reports.EventHandlers;

public class ParticipantReportFinishedDomainEventHandler : INotificationHandler<ParticipantReportFinishedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantReportFinishedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ParticipantReportFinishedEvent notification, CancellationToken cancellationToken)
    {
        var participants = await _unitOfWork.ParticipantRepository.Value.FindAsync(
            new ParticipantsByUserIdSpecification(notification.UserId));
        foreach (var participant in notification.Participants)
        {
            var participantToUpdate =
                participant.Id.HasValue ? participants.FirstOrDefault(x => x.Id == participant.Id) : null;

            switch (participant.ElementType)
            {
                case ElementType.New:
                    participantToUpdate = new Participant(notification.UserId, participant.Name, participant.VkId,
                        participant.Type);
                    await _unitOfWork.ParticipantRepository.Value.AddAsync(participantToUpdate);
                    break;
                case ElementType.Rename when participantToUpdate == null:
                    continue;
                case ElementType.Rename:
                    participantToUpdate.UpdateName(participant.Name);
                    await _unitOfWork.ParticipantRepository.Value.UpdateAsync(participantToUpdate);
                    break;
                case ElementType.Leave when participantToUpdate == null:
                    continue;
                case ElementType.Leave:
                    await _unitOfWork.ParticipantRepository.Value.DeleteAsync(participantToUpdate.Id);
                    break;
            }
        }
    }
}