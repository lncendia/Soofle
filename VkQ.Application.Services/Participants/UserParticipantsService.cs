using VkQ.Application.Abstractions.Participants.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Application.Services.Participants;

public class UserParticipantsService : IUserParticipantsService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserParticipantsService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<List<(Guid id, string name)>> GetUserParticipantsAsync(Guid userId)
    {
        ISpecification<Participant, IParticipantSpecificationVisitor> participantsSpec =
            new ParticipantsByUserIdSpecification(userId);
        participantsSpec =
            new AndSpecification<Participant, IParticipantSpecificationVisitor>(participantsSpec,
                new ParentParticipantsSpecification());
        var participants = await _unitOfWork.ParticipantRepository.Value.FindAsync(participantsSpec);
        return participants.Select(x => (x.Id, x.Name)).ToList();
    }
}