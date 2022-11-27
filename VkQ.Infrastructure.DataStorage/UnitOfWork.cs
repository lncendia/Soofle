using VkQ.Domain.Abstractions.Repositories;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Infrastructure.DataStorage.Context;
using VkQ.Infrastructure.DataStorage.Factories.AggregateFactories;
using VkQ.Infrastructure.DataStorage.Factories.ModelFactories;
using VkQ.Infrastructure.DataStorage.Repositories;

namespace VkQ.Infrastructure.DataStorage;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public Lazy<IUserRepository> UserRepository => new(() =>
        new UserRepository(_context, new UserFactory(), new UserModelFactory(_context)));

    public Lazy<ILinkRepository> LinkRepository => new(() =>
        new LinkRepository(_context, new LinkFactory(), new LinkModelFactory(_context)));

    public Lazy<IProxyRepository> ProxyRepository => new(() =>
        new ProxyRepository(_context, new ProxyFactory(), new ProxyModelFactory(_context)));

    public Lazy<IParticipantRepository> ParticipantRepository => new(() =>
        new ParticipantRepository(_context, new ParticipantFactory(), new ParticipantModelFactory(_context)));

    public Lazy<IReportLogRepository> ReportLogRepository => new(() =>
        new ReportLogRepository(_context, new ReportLogFactory(), new ReportLogModelFactory(_context)));

    public Lazy<ILikeReportRepository> LikeReportRepository => new(() =>
        new LikeReportRepository(_context, new LikeReportFactory(), new LikeReportModelFactory(_context)));

    public Lazy<IParticipantReportRepository> ParticipantReportRepository => new(() =>
        new ParticipantReportRepository(_context, new ParticipantReportFactory(),
            new ParticipantReportModelFactory(_context)));

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}