using VkQ.Application.Abstractions.Participants.DTOs;
using VkQ.Application.Abstractions.Participants.Exceptions;
using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Enums;

namespace VkQ.Application.Services.Services.Participants;

public class ParticipantService : IParticipantService
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public Task<List<ParticipantsDto>> GetParticipantsAsync(Guid userId, int page, string? name, ParticipantType? type,
        bool? vip)
    {
        
    }

    public async Task EditParticipantAsync(Guid userId, Guid participantId, string? note, bool? vip)
    {
        var participant = await _unitOfWork.ParticipantRepository.Value.GetAsync(participantId);
        if (participant == null) throw new ParticipantNotFoundException();
        if (participant.UserId != userId) throw new ParticipantNotFoundException();
        if (!string.IsNullOrEmpty(note)) participant.SetNotes(note);
        if (vip.HasValue) participant.SetVip(vip.Value);
        await _unitOfWork.ParticipantRepository.Value.UpdateAsync(participant);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteParticipantAsync(Guid userId, Guid participantId)
    {
        var participant = await _unitOfWork.ParticipantRepository.Value.GetAsync(participantId);
        if (participant == null) throw new ParticipantNotFoundException();
        if (participant.UserId != userId) throw new ParticipantNotFoundException();
        await _unitOfWork.ParticipantRepository.Value.DeleteAsync(participantId);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task<ParticipantsDto> GetParticipantAsync(Guid userId, Guid participantId)
    {
        throw new NotImplementedException();
    }
}