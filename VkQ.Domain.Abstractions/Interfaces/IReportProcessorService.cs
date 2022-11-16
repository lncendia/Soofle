using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Domain.Abstractions.Interfaces;

public interface IReportProcessorService<in T> where T : Report
{
    Task ProcessReportAsync(T report, CancellationToken token);
}