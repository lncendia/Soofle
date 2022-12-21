using VkQ.Application.Abstractions.ReportsQuery.DTOs;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Specification;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Application.Services.ReportsQuery;

public class ReportManager : IReportManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportMapper _mapper;

    public ReportManager(IUnitOfWork unitOfWork, IReportMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ReportShortDto>> FindAsync(Guid userId, SearchQuery query)
    {
        ISpecification<ReportLog, IReportLogSpecificationVisitor> specification = new LogByUserIdSpecification(userId);
        if (query.Hashtag != null)
            specification =
                new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(specification,
                    new LogByInfoSpecification(query.Hashtag));
        if (query.From.HasValue || query.To.HasValue)
            specification = new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(specification,
                new LogByCreationDateSpecification(query.To ?? DateTimeOffset.Now,
                    query.From ?? DateTimeOffset.MinValue));
        if (query.ReportType.HasValue)
            specification = new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(specification,
                new LogByReportTypeSpecification(query.ReportType.Value));
        var reports =
            await _unitOfWork.ReportLogRepository.Value.FindAsync(specification, null, (query.Page - 1) * 15, 15);
        return reports.Select(x => new ReportShortDto(x.ReportId, x.AdditionalInfo, x.Type, x.CreatedAt, x.FinishedAt,
            x.Success.HasValue, x.Success ?? false)).ToList();
    }

    public async Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();

        return _mapper.LikeReportMapper.Value.Map(report);
    }


    public async Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();

        return _mapper.ParticipantReportMapper.Value.Map(report);
    }
}