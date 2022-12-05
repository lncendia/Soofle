using VkQ.Application.Abstractions.Reports.DTOs.Reports;
using VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;
using VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;

namespace VkQ.Application.Abstractions.Reports.ServicesInterfaces;

public interface IReportManager
{
    public Task<List<ReportDto>> GetReportsAsync(Guid userId);
    public Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId);
    public Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId);
}