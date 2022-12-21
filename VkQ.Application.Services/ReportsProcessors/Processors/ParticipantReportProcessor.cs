using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Services.ReportsProcessors.StaticMethods;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.BaseReport.Exceptions;
using VkQ.Domain.Reposts.ParticipantReport.DTOs;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.ReportsProcessors.Processors;

public class ParticipantReportProcessor : IReportProcessorUnit<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IParticipantsGetterService _participantsGetterService;

    public ParticipantReportProcessor(IUnitOfWork unitOfWork, IParticipantsGetterService participantsGetterService)
    {
        _unitOfWork = unitOfWork;
        _participantsGetterService = participantsGetterService;
    }

    public async Task ProcessReportAsync(ParticipantReport report, CancellationToken token)
    {
        if (!report.IsStarted) throw new ReportNotStartedException(report.Id);
        if (report.IsCompleted) throw new ReportAlreadyCompletedException(report.Id);
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);

        var elements = await _participantsGetterService.GetParticipantsAsync(info, report.VkId, token);
        foreach (var participantDto in elements)
        {
            report.ProcessParticipantInfo(new ParticipantDto(participantDto.VkId, participantDto.Name,
                participantDto.Type));
        }

        await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }
}