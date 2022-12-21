using System.Linq.Expressions;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Infrastructure.DataStorage.Models;

namespace VkQ.Infrastructure.DataStorage.Visitors.Specifications;

internal class ParticipantVisitor : BaseVisitor<ParticipantModel, IParticipantSpecificationVisitor, Participant>,
    IParticipantSpecificationVisitor
{
    protected override Expression<Func<ParticipantModel, bool>> ConvertSpecToExpression(
        ISpecification<Participant, IParticipantSpecificationVisitor> spec)
    {
        var visitor = new ParticipantVisitor();
        spec.Accept(visitor);
        return visitor.Expr!;
    }

    public void Visit(ParticipantsByUserIdSpecification specification) => Expr = x => x.UserId == specification.UserId;
    public void Visit(ParticipantsByNameSpecification specification) => Expr = x => x.Name.Contains(specification.Name);

    public void Visit(ParticipantsByTypeSpecification specification) => Expr = x => x.Type == specification.Type;

    public void Visit(VipParticipantsSpecification specification) => Expr = x => x.Vip;

    public void Visit(ParentParticipantsSpecification specification) => Expr = x => !x.ParentParticipantId.HasValue;
}