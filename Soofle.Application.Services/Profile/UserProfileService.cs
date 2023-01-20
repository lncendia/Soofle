using Soofle.Application.Abstractions.Profile.DTOs;
using Soofle.Application.Abstractions.Profile.ServicesInterfaces;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Links.Specification;
using Soofle.Domain.Ordering;
using Soofle.Domain.Participants.Specification;
using Soofle.Domain.ReportLogs.Entities;
using Soofle.Domain.ReportLogs.Specification;
using Soofle.Domain.ReportLogs.Specification.Visitor;
using Soofle.Domain.Specifications;
using Soofle.Domain.Specifications.Abstractions;
using Soofle.Domain.Transactions.Entities;
using Soofle.Domain.Transactions.Ordering;
using Soofle.Domain.Transactions.Ordering.Visitor;
using Soofle.Domain.Transactions.Specification;
using Soofle.Domain.Users.Entities;
using Soofle.Domain.Users.Specification;
using Soofle.Domain.Users.Specification.Visitor;

namespace Soofle.Application.Services.Profile;

public class UserProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<ProfileDto> GetAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var links = await GetLinksAsync(userId);
        var payments = await GetPaymentsAsync(userId);
        var reportsCount = await GetReportsCountAsync(userId);
        var lastMonthReportsCount = await GetLastMonthReportsCountAsync(userId);
        var participantsCount = await GetParticipantsCountAsync(userId);
        var vk = user.HasVk ? new VkDto(user.Vk!.Login, user.Vk.Password, user.Vk.IsActive) : null;
        var stats = new StatsDto(participantsCount, reportsCount, lastMonthReportsCount);
        return new ProfileDto(vk, user.ChatId, stats, links, payments, user.Subscription?.SubscriptionDate,
            user.Subscription?.ExpirationDate);
    }

    private Task<int> GetLastMonthReportsCountAsync(Guid userId)
    {
        return _unitOfWork.ReportLogRepository.Value.CountAsync(
            new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(new LogByUserIdSpecification(userId),
                new LogByCreationDateSpecification(DateTimeOffset.Now, DateTimeOffset.Now.AddMonths(-1))));
    }

    private Task<int> GetReportsCountAsync(Guid userId) =>
        _unitOfWork.ReportLogRepository.Value.CountAsync(new LogByUserIdSpecification(userId));

    private Task<int> GetParticipantsCountAsync(Guid userId) =>
        _unitOfWork.ParticipantRepository.Value.CountAsync(new ParticipantsByUserIdSpecification(userId));


    private async Task<List<LinkDto>> GetLinksAsync(Guid userId)
    {
        var links = await _unitOfWork.LinkRepository.Value.FindAsync(new LinkByUserIdSpecification(userId));
        if (!links.Any()) return new List<LinkDto>();
        var ids = links.SelectMany(l => new[] { l.User1Id, l.User2Id }).Distinct().ToList();
        ISpecification<User, IUserSpecificationVisitor> spec = new UserByIdSpecification(ids.First());
        spec = ids.Skip(1).Aggregate(spec,
            (current, id) =>
                new OrSpecification<User, IUserSpecificationVisitor>(current, new UserByIdSpecification(id)));

        var users = await _unitOfWork.UserRepository.Value.FindAsync(spec);
        return links.Select(l =>
        {
            var user1 = users.FirstOrDefault(x => x.Id == l.User1Id)?.Name ?? throw new UserNotFoundException();
            var user2 = users.FirstOrDefault(x => x.Id == l.User2Id)?.Name ?? throw new UserNotFoundException();
            return new LinkDto(l.Id, user1, user2, l.IsConfirmed, l.User1Id == userId);
        }).ToList();
    }

    private async Task<List<PaymentDto>> GetPaymentsAsync(Guid userId)
    {
        var payments = await _unitOfWork.TransactionRepository.Value.FindAsync(
            new TransactionByUserIdSpecification(userId),
            new DescendingOrder<Transaction, ITransactionSortingVisitor>(new TransactionByCreationDateOrder()));

        return payments.Select(x =>
                new PaymentDto(x.Id, x.PaymentSystemId, x.Amount, x.CreationDate, x.IsSuccessful, x.ConfirmationDate,
                    x.PaymentSystemUrl))
            .ToList();
    }
}