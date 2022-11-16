using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Domain.Abstractions.Interfaces;

public interface IReportInitializerService<in T> where T : Report
{
    Task InitializeReportAsync(T report, CancellationToken token);
}