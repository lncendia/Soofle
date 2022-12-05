using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportProcessorService
{
    Lazy<IReportProcessorUnit<LikeReport>> LikeReportProcessor { get; }
    Lazy<IReportProcessorUnit<ParticipantReport>> ParticipantReportProcessor { get; }
}