using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportInitializerService
{
    Lazy<IReportInitializerUnit<LikeReport>> LikeReportInitializer { get; }
    Lazy<IReportInitializerUnit<ParticipantReport>> ParticipantReportInitializer { get; }
}