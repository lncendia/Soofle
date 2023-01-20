using Soofle.Application.Abstractions.Jobs.ServicesInterfaces;
using Soofle.Application.Abstractions.Links.Exceptions;
using Soofle.Application.Abstractions.ReportsManagement.DTOs;
using Soofle.Application.Abstractions.ReportsManagement.Exceptions;
using Soofle.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsQuery.Exceptions;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Links.Entities;
using Soofle.Domain.Links.Specification;
using Soofle.Domain.Links.Specification.Visitor;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;
using Soofle.Domain.Specifications;
using Soofle.Domain.Specifications.Abstractions;

namespace Soofle.Application.Services.ReportsManagement;

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

    public async Task CreateLikeReportAsync(PublicationReportCreateDto dto, TimeSpan? startAt = null)
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
        DateTimeOffset? startDate = startAt.HasValue ? DateTimeOffset.Now.Add(startAt.Value) : null;
        var key = await _jobScheduler.StartLikeReportAsync(report.Id, startDate);
        await _jobStorage.StoreJobIdAsync(report.Id, key);
    }

    public async Task CreateCommentReportAsync(PublicationReportCreateDto dto, TimeSpan? startAt = null)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(dto.UserId);
        if (user == null) throw new UserNotFoundException();
        List<Link>? links = null;
        if (dto.CoAuthors?.Any() ?? false)
        {
            links = await GetLinks(dto.CoAuthors, dto.UserId);
            if (links!.Count < dto.CoAuthors.Count) throw new LinkNotFoundException();
        }

        var report = new CommentReport(user, dto.Hashtag, dto.SearchStartDate, links);
        await _unitOfWork.CommentReportRepository.Value.AddAsync(report);
        await _unitOfWork.SaveChangesAsync();
        DateTimeOffset? startDate = startAt.HasValue ? DateTimeOffset.Now.Add(startAt.Value) : null;
        var key = await _jobScheduler.StartCommentReportAsync(report.Id, startDate);
        await _jobStorage.StoreJobIdAsync(report.Id, key);
    }

    public async Task CreateParticipantReportAsync(Guid userId, TimeSpan? startAt = null)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var report = new ParticipantReport(user);
        await _unitOfWork.ParticipantReportRepository.Value.AddAsync(report);
        await _unitOfWork.SaveChangesAsync();
        DateTimeOffset? startDate = startAt.HasValue ? DateTimeOffset.Now.Add(startAt.Value) : null;
        var key = await _jobScheduler.StartParticipantReportAsync(report.Id, startDate);
        await _jobStorage.StoreJobIdAsync(report.Id, key);
    }

    public async Task CancelLikeReportAsync(Guid reportId, Guid userId)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        if (!report.IsCompleted) await CancelJobAsync(reportId);
        await _unitOfWork.LikeReportRepository.Value.DeleteAsync(reportId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelCommentReportAsync(Guid reportId, Guid userId)
    {
        var report = await _unitOfWork.CommentReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        if (!report.IsCompleted) await CancelJobAsync(reportId);
        await _unitOfWork.CommentReportRepository.Value.DeleteAsync(reportId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelParticipantReportAsync(Guid reportId, Guid userId)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(reportId);
        if (report == null) throw new ReportNotFoundException();
        if (report.UserId != userId) throw new ReportNotFoundException();
        if (!report.IsCompleted) await CancelJobAsync(reportId);
        await _unitOfWork.ParticipantReportRepository.Value.DeleteAsync(reportId);
        await _unitOfWork.SaveChangesAsync();
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

    private async Task CancelJobAsync(Guid reportId)
    {
        var jobId = await _jobStorage.GetJobIdAsync(reportId);
        if (string.IsNullOrEmpty(jobId)) throw new StoppingJobException();
        var result = await _jobScheduler.CancelAsync(jobId);
        if (!result) throw new StoppingJobException();
        await _jobStorage.DeleteJobIdAsync(reportId);
    }
}