using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportInitializerService
{
    Lazy<IReportInitializerUnit<LikeReport>> LikeReportInitializer { get; }
    Lazy<IReportInitializerUnit<ParticipantReport>> ParticipantReportInitializer { get; }
    Lazy<IReportInitializerUnit<CommentReport>> CommentReportInitializer { get; }
}