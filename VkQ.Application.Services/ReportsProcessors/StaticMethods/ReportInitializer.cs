using VkQ.Application.Abstractions.ReportsProcessors.DTOs;
using VkQ.Application.Abstractions.ReportsProcessors.Exceptions;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.PublicationReport.DTOs;
using VkQ.Domain.Reposts.PublicationReport.Entities;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification;
using VkQ.Domain.Users.Specification.Visitor;
using PublicationDto = VkQ.Domain.Reposts.PublicationReport.DTOs.PublicationDto;

namespace VkQ.Application.Services.ReportsProcessors.StaticMethods;

internal static class ReportInitializer
{
    public static async Task<IEnumerable<PublicationDto>> GetPublicationsAsync(PublicationReport report,
        RequestInfo info, IPublicationGetterService publicationGetterService, CancellationToken token)
    {
        var publications =
            await publicationGetterService.GetPublicationsAsync(info, report.Hashtag, 1000, report.SearchStartDate,
                token);
        return publications.Select(x => new PublicationDto(x.PublicationId, x.OwnerId));
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public static async Task<IEnumerable<ChatParticipants>> GetParticipantsAsync(LikeReport report, IUnitOfWork unitOfWork)
    {
        ISpecification<User, IUserSpecificationVisitor> userSpec = new UserByIdSpecification(report.UserId);

        ISpecification<Participant, IParticipantSpecificationVisitor> participantSpec =
            new ParticipantsByUserIdSpecification(report.UserId);
        var users = report.LinkedUsers;

        participantSpec = users.Aggregate(participantSpec,
            (current, user) =>
                new OrSpecification<Participant, IParticipantSpecificationVisitor>(current,
                    new ParticipantsByUserIdSpecification(user)));
        userSpec = users.Aggregate(userSpec,
            (current, user) => new OrSpecification<User, IUserSpecificationVisitor>(current,
                new UserByIdSpecification(user)));

        var participants = unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);
        var chats = unitOfWork.UserRepository.Value.FindAsync(userSpec);
        await Task.WhenAll(participants, chats);
        return participants.Result.GroupBy(x => x.UserId)
            .Select(x => new ChatParticipants(x.ToList(),
                chats.Result.FirstOrDefault(y => y.Id == x.Key)?.Name ?? throw new LinkedUserNotFoundException(x.Key)));
    }
}