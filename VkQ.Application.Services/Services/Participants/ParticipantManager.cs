using VkQ.Application.Abstractions.Participants.DTOs;
using VkQ.Application.Abstractions.Participants.Exceptions;
using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Application.Services.Services.Participants;

public class ParticipantManager : IParticipantManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantManager(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<ParticipantDto>> FindAsync(Guid userId, SearchQuery query)
    {
        ISpecification<Participant, IParticipantSpecificationVisitor> participantsSpec =
            new ParticipantsByUserIdSpecification(userId);
        var participants = await _unitOfWork.ParticipantRepository.Value.FindAsync(participantsSpec);

        var parentParticipants = participants.Where(x => !x.ParentParticipantId.HasValue).ToList();
        var skip = (query.Page - 1) * 100;
        if (parentParticipants.Count <= skip) return new List<ParticipantDto>();
        var childParticipants = participants.Where(x => x.ParentParticipantId.HasValue)
            .GroupBy(x => x.ParentParticipantId).ToList();

        var list = new List<ParticipantDto>();

        foreach (var parentParticipant in parentParticipants)
        {
            var children = childParticipants.FirstOrDefault(x => x.Key == parentParticipant.Id)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsValid(parentParticipant, query);
            var validChildren = children?.Where(x => IsValid(x, query)).Select(x => Map(x, null)).ToList();
            if (valid || validChildren?.Any() == true)
            {
                list.Add(Map(parentParticipant, validChildren));
            }
        }

        return list.Skip(skip).Take(100).ToList();
    }

    private static bool IsValid(Participant participant, SearchQuery query)
    {
        if (query.Type.HasValue && participant.Type != query.Type.Value)
            return false;

        if (query.Vip.HasValue && participant.Vip != query.Vip.Value)
            return false;

        if (!string.IsNullOrEmpty(query.Name) && !participant.Name.Contains(query.Name))
            return false;

        return true;
    }

    private static ParticipantDto Map(Participant participant, IEnumerable<ParticipantDto>? children) =>
        new(participant.Id, participant.Name, participant.Notes, participant.Type, participant.VkId, participant.Vip,
            children);

    public async Task EditAsync(Guid userId, Guid participantId, Guid? parentId, string? note, bool vip)
    {
        var participant = await _unitOfWork.ParticipantRepository.Value.GetAsync(participantId);
        if (participant == null) throw new ParticipantNotFoundException();
        if (participant.UserId != userId) throw new ParticipantNotFoundException();

        if (string.IsNullOrEmpty(note)) participant.DeleteNotes();
        else participant.SetNotes(note);

        participant.SetVip(vip);

        if (parentId.HasValue)
        {
            var parent = await _unitOfWork.ParticipantRepository.Value.GetAsync(parentId.Value);
            if (parent == null) throw new ParticipantNotFoundException();
            participant.SetParent(parent);
        }
        else
        {
            participant.DeleteParent();
        }

        await _unitOfWork.ParticipantRepository.Value.UpdateAsync(participant);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task DeleteAsync(Guid userId, Guid participantId)
    {
        var participant = await _unitOfWork.ParticipantRepository.Value.GetAsync(participantId);
        if (participant == null) throw new ParticipantNotFoundException();
        if (participant.UserId != userId) throw new ParticipantNotFoundException();
        await _unitOfWork.ParticipantRepository.Value.DeleteAsync(participantId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<GetParticipantDto> GetAsync(Guid userId, Guid participantId)
    {
        var participant = await _unitOfWork.ParticipantRepository.Value.GetAsync(participantId);
        if (participant == null) throw new ParticipantNotFoundException();
        if (participant.UserId != userId) throw new ParticipantNotFoundException();
        return new GetParticipantDto(participant.Id, participant.ParentParticipantId, participant.Name,
            participant.Notes, participant.Type, participant.VkId, participant.Vip);
    }
}