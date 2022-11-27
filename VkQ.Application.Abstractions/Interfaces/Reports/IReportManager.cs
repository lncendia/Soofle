using VkQ.Application.Abstractions.DTO.Reports;

namespace VkQ.Application.Abstractions.Interfaces.Reports;

public interface IReportManager
{
    public Task<List<ReportDto>> GetReportsAsync(Guid userId);
    public Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId);
    public Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId);
}