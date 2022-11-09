using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport;

namespace VkQ.Domain.Abstractions.Services;

internal interface IReportProcessorService<in T> where T : Report
{
    Task ProcessReportAsync(T report, CancellationToken token);
}