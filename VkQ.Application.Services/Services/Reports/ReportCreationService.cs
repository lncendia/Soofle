using VkQ.Application.Abstractions.Reports.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsManagement.DTOs;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.BackgroundScheduler;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;

namespace VkQ.Application.Services.Services.Reports;

public class ReportCreationService : IReportCreationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationReportBuilder<LikeReport> _reportBuilder;
    private readonly IJobScheduler _jobScheduler;
    private readonly IJobStorage _jobStorage;

    public ReportCreationService(IUnitOfWork unitOfWork, IPublicationReportBuilder<LikeReport> reportBuilder,
        IJobScheduler jobScheduler, IJobStorage jobStorage)
    {
        _unitOfWork = unitOfWork;
        _reportBuilder = reportBuilder;
        _jobScheduler = jobScheduler;
        _jobStorage = jobStorage;
    }

    public async Task CreateLikeReportAsync(LikeReportCreateDto dto, DateTime? startAt = null)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(dto.UserId);
        if (user == null) throw new UserNotFoundException();
        var report = await _reportBuilder.BuildAsync(user, dto.Hashtag, dto.SearchStartDate, dto.CoAuthors);
        await _unitOfWork.LikeReportRepository.Value.AddAsync(report);
        await _unitOfWork.SaveChangesAsync();
        var key = await _jobScheduler.StartLikeReportAsync(report.Id);
        await _jobStorage.StoreJobIdAsync(report.Id, key);
    }

    public Task CreateParticipantReportAsync(ParticipantReportCreateDto dto, DateTime? startAt = null)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteLikeReportAsync(Guid reportId, Guid userId)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        var t1 = _unitOfWork.LikeReportRepository.Value.DeleteAsync(report.Id);
        var t2 = CancelJobAsync(reportId);
        await Task.WhenAll(t1, t2);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteParticipantReportAsync(Guid reportId, Guid userId)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        var t1 = _unitOfWork.ParticipantReportRepository.Value.DeleteAsync(report.Id);
        var t2 = CancelJobAsync(reportId);
        await Task.WhenAll(t1, t2);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task CancelJobAsync(Guid reportId)
    {
        var key = await _jobStorage.GetJobIdAsync(reportId); //todo:exception
        await _jobScheduler.CancelAsync(key);
    }
}