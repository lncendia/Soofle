using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Mappers.AggregateMappers;
using VkQ.Infrastructure.DataStorage.Mappers.ModelMappers;
using VkQ.Infrastructure.DataStorage.Repositories;

namespace VkQ.Infrastructure.DataStorage;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public Lazy<IUserRepository> UserRepository => new(() =>
        new UserRepository(_context, new UserMapper(), new UserModelMapper(_context)));

    public Lazy<ILinkRepository> LinkRepository => new(() =>
        new LinkRepository(_context, new LinkMapper(), new LinkModelMapper(_context)));

    public Lazy<IProxyRepository> ProxyRepository => new(() =>
        new ProxyRepository(_context, new ProxyMapper(), new ProxyModelMapper(_context)));

    public Lazy<ITransactionRepository> TransactionRepository => new(() =>
        new TransactionRepository(_context, new TransactionMapper(), new TransactionModelMapper(_context)));

    public Lazy<IParticipantRepository> ParticipantRepository => new(() =>
        new ParticipantRepository(_context, new ParticipantMapper(), new ParticipantModelMapper(_context)));

    public Lazy<IReportLogRepository> ReportLogRepository => new(() =>
        new ReportLogRepository(_context, new ReportLogMapper(), new ReportLogModelMapper(_context)));

    public Lazy<ILikeReportRepository> LikeReportRepository => new(() =>
        new LikeReportRepository(_context, new LikeReportMapper(), new LikeReportModelMapper(_context)));

    public Lazy<IParticipantReportRepository> ParticipantReportRepository => new(() =>
        new ParticipantReportRepository(_context, new ParticipantReportMapper(),
            new ParticipantReportModelMapper(_context)));

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}