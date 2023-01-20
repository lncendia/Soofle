using Microsoft.Extensions.Caching.Memory;
using Soofle.Application.Abstractions.Participants.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Application.Services.Participants;

public class UserParticipantsService : IUserParticipantsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public UserParticipantsService(IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<List<(Guid id, string name)>> GetUserParticipantsAsync(Guid userId)
    {
        if (!_cache.TryGetValue(CachingConstants.GetParticipantsKey(userId), out List<Participant>? participants))
        {
            ISpecification<Participant, IParticipantSpecificationVisitor> participantsSpec =
                new ParticipantsByUserIdSpecification(userId);
            participantsSpec = new AndSpecification<Participant, IParticipantSpecificationVisitor>(participantsSpec,
                new ParentParticipantsSpecification());
            participants = await _unitOfWork.ParticipantRepository.Value.FindAsync(participantsSpec);
        }
        else
        {
            participants!.RemoveAll(x => x.ParentParticipantId.HasValue);
        }

        return participants.Select(x => (x.Id, x.Name)).ToList();
    }
}