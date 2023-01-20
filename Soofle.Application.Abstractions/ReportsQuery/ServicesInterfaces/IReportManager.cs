using Soofle.Application.Abstractions.ReportsQuery.DTOs;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.CommentReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using Soofle.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;

namespace Soofle.Application.Abstractions.ReportsQuery.ServicesInterfaces;

public interface IReportManager
{
    public Task<List<ReportShortDto>> FindAsync(Guid userId, SearchQuery query);
    public Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId);
    public Task<CommentReportDto> GetCommentReportAsync(Guid userId, Guid reportId);
    public Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId);
    
}