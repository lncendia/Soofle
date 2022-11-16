using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Services.StaticMethods;

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

    public async Task InitializeReportAsync(LikeReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        await PublicationLoader.LoadPublicationsAsync(report, info, _publicationGetterService, token);
        await LoadParticipantsAsync(report);
    }

    private async Task LoadParticipantsAsync(LikeReport report)
    {
        var participants =
            await _unitOfWork.ParticipantRepository.Value.FindAsync(
                new ParticipantsByUserIdSpecification(report.UserId));
        report.LoadElements(participants);
    }
}