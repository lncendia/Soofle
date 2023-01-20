using System.Linq.Expressions;
using Soofle.Infrastructure.DataStorage.Models;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Visitors.Specifications;

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

    public void Visit(ChildParticipantsSpecification specification) =>
        Expr = x => x.ParentParticipantId == specification.ParentId;
}