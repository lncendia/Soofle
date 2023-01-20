using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Participants.Entities;
using Soofle.Domain.Participants.Specification;
using Soofle.Domain.Participants.Specification.Visitor;
using Soofle.Domain.Reposts.PublicationReport.DTOs;
using Soofle.Domain.Reposts.PublicationReport.Entities;
using Soofle.Domain.Specifications;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification;
using Soofle.Domain.Users.Specification.Visitor;
using PublicationDto = Soofle.Domain.Reposts.PublicationReport.DTOs.PublicationDto;

namespace Soofle.Application.Services.ReportsProcessors.StaticMethods;

internal static class ReportInitializer
{
    public static async Task<IEnumerable<PublicationDto>> GetPublicationsAsync(PublicationReport report,
        RequestInfo info, IPublicationService publicationGetterService, CancellationToken token)
    {
        var publications =
            await publicationGetterService.GetAsync(info, report.Hashtag, 500, report.SearchStartDate, token);
        return publications.Select(x => new PublicationDto(x.PublicationId, x.OwnerId));
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public static async Task<IEnumerable<ChatParticipants>> GetParticipantsAsync(PublicationReport report,
        IUnitOfWork unitOfWork)
    {
        ISpecification<User, IUserSpecificationVisitor> userSpec = new UserByIdSpecification(report.UserId);

        ISpecification<Participant, IParticipantSpecificationVisitor> participantSpec =
            new ParticipantsByUserIdSpecification(report.UserId);

        participantSpec = report.LinkedUsers.Aggregate(participantSpec,
            (current, user) =>
                new OrSpecification<Participant, IParticipantSpecificationVisitor>(current,
                    new ParticipantsByUserIdSpecification(user)));
        userSpec = report.LinkedUsers.Aggregate(userSpec,
            (current, user) => new OrSpecification<User, IUserSpecificationVisitor>(current,
                new UserByIdSpecification(user)));

        var participants = await unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);
        var chats = await unitOfWork.UserRepository.Value.FindAsync(userSpec);
        return participants.GroupBy(x => x.UserId)
            .Select(x => new ChatParticipants(x.ToList(),
                chats.FirstOrDefault(y => y.Id == x.Key)?.Name ?? throw new LinkedUserNotFoundException(x.Key)));
    }
}