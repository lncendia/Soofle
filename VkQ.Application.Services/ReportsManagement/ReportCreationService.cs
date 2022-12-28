using VkQ.Application.Abstractions.Links.Exceptions;
using VkQ.Application.Abstractions.ReportsManagement.DTOs;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.BackgroundScheduler;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Entities;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Links.Specification.Visitor;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;

namespace VkQ.Application.Services.ReportsManagement;

public class ReportCreationService : IReportCreationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJobScheduler _jobScheduler;
    private readonly IJobStorage _jobStorage;

    public ReportCreationService(IUnitOfWork unitOfWork, IJobScheduler jobScheduler, IJobStorage jobStorage)
    {
        _unitOfWork = unitOfWork;
        _jobScheduler = jobScheduler;
        _jobStorage = jobStorage;
    }

    public async Task CreateLikeReportAsync(PublicationReportCreateDto dto, DateTime? startAt = null)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(dto.UserId);
        if (user == null) throw new UserNotFoundException();
        List<Link>? links = null;
        if (dto.CoAuthors?.Any() ?? false)
        {
            links = await GetLinks(dto.CoAuthors, dto.UserId);
            if (links!.Count < dto.CoAuthors.Count) throw new LinkNotFoundException();
        }

        var report = new LikeReport(user, dto.Hashtag, dto.SearchStartDate, links);
        await _unitOfWork.LikeReportRepository.Value.AddAsync(report);
        await _unitOfWork.SaveChangesAsync();
        var key = await _jobScheduler.StartLikeReportAsync(report.Id);
        await _jobStorage.StoreJobIdAsync(report.Id, key);
    }

    public async Task CreateParticipantReportAsync(Guid userId, DateTime? startAt = null)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var report = new ParticipantReport(user);
        await _unitOfWork.ParticipantReportRepository.Value.AddAsync(report);
        await _unitOfWork.SaveChangesAsync();
        var key = await _jobScheduler.StartParticipantReportAsync(report.Id);
        await _jobStorage.StoreJobIdAsync(report.Id, key);
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

    private async Task<List<Link>?> GetLinks(IReadOnlyCollection<Guid> ids, Guid userId)
    {
        ISpecification<Link, ILinkSpecificationVisitor> spec = new LinkByUserIdsSpecification(userId, ids.First());
        spec = ids.Skip(1).Aggregate(spec,
            (current, id) =>
                new OrSpecification<Link, ILinkSpecificationVisitor>(current,
                    new LinkByUserIdsSpecification(id, userId)));

        spec = new AndSpecification<Link, ILinkSpecificationVisitor>(spec, new AcceptedLinkSpecification());
        return await _unitOfWork.LinkRepository.Value.FindAsync(spec);
    }
}