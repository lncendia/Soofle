using Soofle.Domain.Abstractions.Repositories;

namespace Soofle.Domain.Abstractions.UnitOfWorks;

public interface IUnitOfWork
{
    Lazy<IUserRepository> UserRepository { get; }
    Lazy<ILinkRepository> LinkRepository { get; }
    Lazy<IProxyRepository> ProxyRepository { get; }
    Lazy<IParticipantRepository> ParticipantRepository { get; }
    Lazy<IReportLogRepository> ReportLogRepository { get; }
    Lazy<ILikeReportRepository> LikeReportRepository { get; }
    Lazy<ICommentReportRepository> CommentReportRepository { get; }
    Lazy<IParticipantReportRepository> ParticipantReportRepository { get; }
    Lazy<ITransactionRepository> TransactionRepository { get; }
    Task SaveChangesAsync();
}