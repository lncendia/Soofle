using Microsoft.Extensions.Caching.Memory;
using Soofle.Application.Abstractions.Participants.DTOs;
using Soofle.Application.Abstractions.Participants.Exceptions;
using Soofle.Application.Abstractions.Participants.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification;

namespace Soofle.Application.Services.Participants;

public class ParticipantManager : IParticipantManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IParticipantMapper _participantMapper;
    private readonly IMemoryCache _cache;

    public ParticipantManager(IUnitOfWork unitOfWork, IParticipantMapper participantMapper, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _participantMapper = participantMapper;
        _cache = cache;
    }

    public async Task<List<ParticipantDto>> FindAsync(Guid userId, SearchQuery query)
    {
        if (!_cache.TryGetValue(CachingConstants.GetParticipantsKey(userId), out List<Participant>? allParticipants))
        {
            allParticipants =
                await _unitOfWork.ParticipantRepository.Value.FindAsync(new ParticipantsByUserIdSpecification(userId));
            if (allParticipants.Any()) _cache.Set(CachingConstants.GetParticipantsKey(userId), allParticipants);
        }

        var participants = allParticipants!.GroupBy(x => x.ParentParticipantId).ToList();

        var list = new List<Participant>();
        if (!participants.Any()) return new List<ParticipantDto>();
        foreach (var parentParticipant in participants.First(x => x.Key == null).OrderBy(x=>x.Name))
        {
            var children = participants.FirstOrDefault(x => x.Key == parentParticipant.Id)?.ToList();
            if (query.HasChildren.HasValue)
            {
                if (query.HasChildren.Value && children == null) continue;
                if (!query.HasChildren.Value && children != null) continue;
            }

            var valid = IsValid(parentParticipant, query);
            var validChildren = children?.Where(x => IsValid(x, query)).OrderBy(x=>x.Name).ToList();
            if (valid)
            {
                list.Add(parentParticipant);
                if (children != null) list.AddRange(children);
            }
            else if (validChildren != null && validChildren.Any())
            {
                list.Add(parentParticipant);
                list.AddRange(validChildren);
            }
        }

        return _participantMapper.Map(list.Skip((query.Page - 1) * 100).Take(100).ToList());
    }

    private static bool IsValid(Participant participant, SearchQuery query)
    {
        if (query.Type.HasValue && participant.Type != query.Type.Value)
            return false;

        if (query.Vip.HasValue && participant.Vip != query.Vip.Value)
            return false;

        if (!string.IsNullOrEmpty(query.NameNormalized))
        {
            var b1 = participant.Name.ToUpper().Contains(query.NameNormalized);
            var b2 = participant.VkId.ToString().Contains(query.NameNormalized);
            if (!(b1 || b2)) return false;
        }

        return true;
    }

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
            var children =
                await _unitOfWork.ParticipantRepository.Value.FindAsync(
                    new ChildParticipantsSpecification(participantId));
            participant.SetParent(parent, children);
        }
        else
        {
            participant.DeleteParent();
        }

        _cache.Remove(CachingConstants.GetParticipantsKey(userId));
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