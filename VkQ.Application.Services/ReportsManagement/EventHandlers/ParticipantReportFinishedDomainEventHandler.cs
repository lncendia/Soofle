using MediatR;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.Domain.Reposts.ParticipantReport.Events;

namespace VkQ.Application.Services.ReportsManagement.EventHandlers;

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
            if (participant.ElementType == ElementType.New)
            {
                try
                {
                    var newParticipant = new Participant(notification.UserId, participant.Name, participant.VkId,
                        participant.Type, participants);
                    participants.Add(newParticipant);
                    await _unitOfWork.ParticipantRepository.Value.AddAsync(newParticipant);
                }
                catch
                {
                    // ignored
                }

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

        await _unitOfWork.SaveChangesAsync();
    }
}