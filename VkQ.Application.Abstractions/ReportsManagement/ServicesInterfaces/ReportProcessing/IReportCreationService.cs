using VkQ.Application.Abstractions.ReportsManagement.DTOs;

namespace VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;

public interface IReportCreationService
{
    Task CreateLikeReportAsync(PublicationReportCreateDto dto, DateTime? startAt = null);
    Task CreateParticipantReportAsync(Guid userId, DateTime? startAt = null);
    Task DeleteLikeReportAsync(Guid reportId, Guid userId);
    Task DeleteParticipantReportAsync(Guid reportId, Guid userId);
}