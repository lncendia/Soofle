using VkQ.Domain.Abstractions.Repositories;

namespace VkQ.Domain.Abstractions.UnitOfWorks;

public interface IUnitOfWork
{
    Lazy<IUserRepository> UserRepository { get; }
    Lazy<ILinkRepository> LinkRepository { get; }
    Lazy<IProxyRepository> ProxyRepository { get; }
    Lazy<IParticipantRepository> ParticipantRepository { get; }
    Lazy<IReportLogRepository> ReportLogRepository { get; }
    Lazy<ILikeReportRepository> LikeReportRepository { get; }
    Lazy<IParticipantReportRepository> ParticipantReportRepository { get; }
    Lazy<ITransactionRepository> TransactionRepository { get; }
    Task SaveChangesAsync();
}