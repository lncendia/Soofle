using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Participants.Entities;
using VkQ.Domain.Reposts.ParticipantReport;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Domain.Services.Services;

public class ParticipantReportProcessorService : IReportProcessorService<ParticipantReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantReportProcessorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessReportAsync(ParticipantReport report, CancellationToken token)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(report.UserId);
        if (user?.Vk?.IsActive() ?? true) throw new VkIsNotActiveException();
        if (!user.Vk.ProxyId.HasValue) throw new ProxyIsNotSetException();
        var proxy = await _unitOfWork.ProxyRepository.Value.GetAsync(user.Vk.ProxyId.Value);
        var publications = await _publicationGetterService.GetPublicationsAsync
    }
}