using VkQ.Application.Abstractions.ReportsProcessors.DTOs;
using VkQ.Domain.Reposts.ParticipantReport.DTOs;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IParticipantsGetterService
{
    Task<List<ParticipantDto>> GetParticipantsAsync(RequestInfo data, long id, CancellationToken token);
}