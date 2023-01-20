using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Application.Services.ReportsProcessors.StaticMethods;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.BaseReport.Exceptions;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.ReportsProcessors.Processors;

public class ParticipantReportProcessor : IReportProcessorUnit<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IParticipantsService _participantsGetterService;

    public ParticipantReportProcessor(IUnitOfWork unitOfWork, IParticipantsService participantsGetterService)
    {
        _unitOfWork = unitOfWork;
        _participantsGetterService = participantsGetterService;
    }

    public async Task ProcessReportAsync(ParticipantReport report, CancellationToken token)
    {
        if (!report.IsStarted) throw new ReportNotStartedException(report.Id);
        if (report.IsCompleted) throw new ReportAlreadyCompletedException(report.Id);
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);

        var friends = _participantsGetterService.GetFriendsAsync(info, report.VkId, 500, token);
        var subscriptions = _participantsGetterService.GetSubscriptionsAsync(info, report.VkId, 200, token);
        await Task.WhenAll(friends, subscriptions);
        foreach (var participantDto in friends.Result.Concat(subscriptions.Result))
        {
            report.ProcessParticipantInfo(participantDto.VkId, participantDto.Name, participantDto.Type);
        }
    }
}