using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Participants.Specification.Visitor;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Services.StaticMethods;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification;
using VkQ.Domain.Users.Specification.Visitor;

namespace VkQ.Domain.Services.Services.Initializers;

public class LikeReportInitializerService : IReportInitializerService<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationGetterService _publicationGetterService;

    public LikeReportInitializerService(IUnitOfWork unitOfWork, IPublicationGetterService publicationGetterService)
    {
        _unitOfWork = unitOfWork;
        _publicationGetterService = publicationGetterService;
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public async Task InitializeReportAsync(LikeReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        var t1 = PublicationLoader.LoadPublicationsAsync(report, info, _publicationGetterService, token);
        var t2 = LoadParticipantsAsync(report);
        await Task.WhenAll(t1, t2);
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    private async Task LoadParticipantsAsync(LikeReport report)
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

        var participants = _unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);
        var chats = _unitOfWork.UserRepository.Value.FindAsync(userSpec);
        await Task.WhenAll(participants, chats);

        try
        {
            var participantsList = participants.Result.GroupBy(x => x.UserId)
                .Select(x => (x.AsEnumerable(), chats.Result.First(y => y.Id == x.Key).Name)).ToList();
            report.LoadElements(participantsList);
        }
        catch (InvalidOperationException)
        {
            throw new LinkedUserNotFoundException();
        }
    }
}