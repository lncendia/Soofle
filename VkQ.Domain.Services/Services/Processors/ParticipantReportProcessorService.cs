using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.ParticipantReport.DTOs;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.Domain.Services.StaticMethods;

namespace VkQ.Domain.Services.Services.Processors;

public class ParticipantReportProcessorService : IReportProcessorService<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IParticipantsGetterService _participantsGetterService;

    public ParticipantReportProcessorService(IUnitOfWork unitOfWork,
        IParticipantsGetterService participantsGetterService)
    {
        _unitOfWork = unitOfWork;
        _participantsGetterService = participantsGetterService;
    }

    public async Task ProcessReportAsync(ParticipantReport report, CancellationToken token)
    {
        if (!report.IsStarted) throw new ReportNotStartedException(report.Id);
        if (report.IsCompleted) throw new ReportAlreadyCompletedException(report.Id);
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        await ProcessParticipantsAsync(report, info, token);
        report.Finish();
        await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task ProcessParticipantsAsync(ParticipantReport report, RequestInfo info, CancellationToken token)
    {
        var participantSpec = new ParticipantsByUserIdSpecification(report.UserId);
        var participants = _unitOfWork.ParticipantRepository.Value.FindAsync(participantSpec);
        var elements = _participantsGetterService.GetParticipantsAsync(info, report.VkId, token);

        await Task.WhenAll(participants, elements);

        var participantsList = new List<AddParticipantDto>();

        foreach (var participant in participants.Result.Where(x => x.ParentParticipantId == null))
        {
            var participantDto = elements.Result.FirstOrDefault(x => x.VkId == participant.VkId);

            var children = participants.Result.Where(x => x.ParentParticipantId == participant.Id)
                .Select(x => GetParticipantDto(x, elements.Result.FirstOrDefault(y => y.VkId == x.VkId)))
                .Where(x => x.Type != ElementType.Stay).ToList();

            if (children.Any())
                participantsList.Add(GetParticipantDto(participant, participantDto, children));
            else
            {
                var addParticipantDto = GetParticipantDto(participant, participantDto);
                if (addParticipantDto.Type != ElementType.Stay) participantsList.Add(addParticipantDto);
            }
        }

        report.AddParticipants(participantsList);
    }

    private static AddParticipantDto GetParticipantDto(Participant participant, ParticipantDto? element,
        List<AddParticipantDto>? children = null)
    {
        if (element == null)
            return new AddParticipantDto(participant.Name, null, participant.VkId, ElementType.Leave, children);
        return element.Name != participant.Name
            ? new AddParticipantDto(element.Name, participant.Name, participant.VkId, ElementType.Rename, children)
            : new AddParticipantDto(participant.Name, null, participant.VkId, ElementType.Stay, children);
    }
}