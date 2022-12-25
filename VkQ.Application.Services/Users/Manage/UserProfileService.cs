using VkQ.Application.Abstractions.Users.DTOs;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Application.Abstractions.Users.ServicesInterfaces.Manage;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Links.Specification;
using VkQ.Domain.Ordering;
using VkQ.Domain.Participants.Specification;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.ReportLogs.Specification;
using VkQ.Domain.ReportLogs.Specification.Visitor;
using VkQ.Domain.Specifications;
using VkQ.Domain.Specifications.Abstractions;
using VkQ.Domain.Transactions.Entities;
using VkQ.Domain.Transactions.Ordering;
using VkQ.Domain.Transactions.Ordering.Visitor;
using VkQ.Domain.Transactions.Specification;
using VkQ.Domain.Users.Entities;
using VkQ.Domain.Users.Specification;
using VkQ.Domain.Users.Specification.Visitor;

namespace VkQ.Application.Services.Users.Manage;

public class UserProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProfileDto> GetAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        var links = await GetLinksAsync(userId);
        var payments = await GetPaymentsAsync(userId);
        var reportsCount = await GetReportsCountAsync(userId);
        var lastMonthReportsCount = await GetLastMonthReportsCountAsync(userId);
        var participantsCount = await GetParticipantsCountAsync(userId);
        return new ProfileDto(links, payments, participantsCount, reportsCount, lastMonthReportsCount,
            user.Subscription?.SubscriptionDate, user.Subscription?.ExpirationDate);
    }

    private Task<int> GetLastMonthReportsCountAsync(Guid userId)
    {
        return _unitOfWork.ReportLogRepository.Value.CountAsync(
            new AndSpecification<ReportLog, IReportLogSpecificationVisitor>(new LogByUserIdSpecification(userId),
                new LogByCreationDateSpecification(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(-31))));
    }

    private Task<int> GetReportsCountAsync(Guid userId)
    {
        return _unitOfWork.ReportLogRepository.Value.CountAsync(new LogByUserIdSpecification(userId));
    }

    private Task<int> GetParticipantsCountAsync(Guid userId)
    {
        return _unitOfWork.ParticipantRepository.Value.CountAsync(new ParticipantsByUserIdSpecification(userId));
    }


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
            return new LinkDto(l.Id, user1, user2, l.IsConfirmed);
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