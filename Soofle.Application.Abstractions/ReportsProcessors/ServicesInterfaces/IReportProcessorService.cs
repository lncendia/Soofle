using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportProcessorService
{
    Lazy<IReportProcessorUnit<LikeReport>> LikeReportProcessor { get; }
    Lazy<IReportProcessorUnit<CommentReport>> CommentReportProcessor { get; }
    Lazy<IReportProcessorUnit<ParticipantReport>> ParticipantReportProcessor { get; }
}