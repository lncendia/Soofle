using VkQ.Application.Abstractions.ReportsQuery.DTOs;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

namespace VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportManager
{
    public Task<List<ReportDto>> FindAsync(Guid userId, SearchQuery query);
    public Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId);
    public Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId);
    
}