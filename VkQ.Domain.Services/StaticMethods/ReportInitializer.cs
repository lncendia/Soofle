using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification;
using VkQ.Domain.Users.Specification.Visitor;
using PublicationDto = VkQ.Domain.Reposts.BaseReport.DTOs.PublicationDto;

namespace VkQ.Domain.Services.StaticMethods;

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
    public static async Task<List<(IEnumerable<Participant>, string name)>> GetParticipantsAsync(LikeReport report, IUnitOfWork unitOfWork)
    {
        ISpecification<User, IUserSpecificationVisitor> userSpec = new UserByIdSpecification(report.UserId);

        ISpecification<Participant, IParticipantSpecificationVisitor> participantSpec =
            new ParticipantsByUserIdSpecification(report.UserId);
        var users = report.LinkedUsers;

        if (users != null)
        {
            participantSpec = users.Aggregate(participantSpec,
                (current, user) =>
                    new OrSpecification<Participant, IParticipantSpecificationVisitor>(current,
                        new ParticipantsByUserIdSpecification(user)));
            userSpec = users.Aggregate(userSpec,
                (current, user) => new OrSpecification<User, IUserSpecificationVisitor>(current,
                    new UserByIdSpecification(user)));
        }

        var participants = unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);
        var chats = unitOfWork.UserRepository.Value.FindAsync(userSpec);
        await Task.WhenAll(participants, chats);

        try
        {
            List<(IEnumerable<Participant>, string name)> participantsList = participants.Result.GroupBy(x => x.UserId)
                .Select(x => (x.AsEnumerable(), chats.Result.First(y => y.Id == x.Key).Name)).ToList();
            return participantsList;
        }
        catch (InvalidOperationException)
        {
            throw new LinkedUserNotFoundException();
        }
    }
}