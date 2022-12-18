using VkQ.Application.Abstractions.ReportsQuery.DTOs;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;

namespace VkQ.Application.Services.Services.ReportsQuery;

public class ReportManager : IReportManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportMapper _mapper;

    public ReportManager(IUnitOfWork unitOfWork, IReportMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<List<ReportDto>> FindAsync(Guid userId, SearchQuery query)
    {
        throw new NotImplementedException();
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