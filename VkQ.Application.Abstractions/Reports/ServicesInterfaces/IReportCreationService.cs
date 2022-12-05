using VkQ.Application.Abstractions.Reports.DTOs.ReportCreate;

namespace VkQ.Application.Abstractions.Reports.ServicesInterfaces;

public interface IReportCreationService
{
    Task CreateLikeReportAsync(LikeReportCreateDto dto, DateTime? startAt = null);
    Task CreateParticipantReportAsync(ParticipantReportCreateDto dto, DateTime? startAt = null);
    Task DeleteLikeReportAsync(Guid reportId, Guid userId);
    Task DeleteParticipantReportAsync(Guid reportId, Guid userId);
}