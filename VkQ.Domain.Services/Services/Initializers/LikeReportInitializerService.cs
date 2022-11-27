using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Reposts.LikeReport.DTOs;
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

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public async Task InitializeReportAsync(LikeReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        var t1 = ReportInitializer.GetPublicationsAsync(report, info, _publicationGetterService, token);
        var t2 = ReportInitializer.GetParticipantsAsync(report, _unitOfWork);
        await Task.WhenAll(t1, t2);
        
        var dtos = t2.Result.SelectMany(x =>
            x.Item1.Where(participant => participant.ParentParticipantId == null)
                .Select(y => GetElementDto(y, x.name, x.Item1)));
        
        report.Start(dtos, t1.Result);
    }

    private static LikeElementDto GetElementDto(Participant participant, string chatName,
        IEnumerable<Participant>? allElements)
    {
        return new LikeElementDto(participant.Name, chatName, participant.VkId, participant.Id,
            allElements?.Where(x => x.ParentParticipantId == participant.Id)
                .Select(y => GetElementDto(y, chatName, null)));
    }
}