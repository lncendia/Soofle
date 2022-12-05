using VkQ.Application.Abstractions.Reports.DTOs.Reports;
using VkQ.Application.Abstractions.Reports.DTOs.Reports.LikeReportDto;
using VkQ.Application.Abstractions.Reports.DTOs.Reports.ParticipantReportDto;
using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;

namespace VkQ.Application.Services.Services.Reports;

public class ReportManager : IReportManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReportManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<List<ReportDto>> GetReportsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<LikeReportDto> GetLikeReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new Exception("Report not found"); //todo: exception

        if (report.UserId != userId) throw new Exception("Access denied"); //todo: exception

        return _mapper.LikeReportMapper.Value.Map(report);
    }

    public async Task<ParticipantReportDto> GetParticipantReportAsync(Guid userId, Guid reportId)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new Exception("Report not found"); //todo: exception

        if (report.UserId != userId) throw new Exception("Access denied"); //todo: exception

        return _mapper.ParticipantReportMapper.Value.Map(report);
    }
}