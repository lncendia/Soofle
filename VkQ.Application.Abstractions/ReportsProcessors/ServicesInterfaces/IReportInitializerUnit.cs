using VkQ.Domain.Reposts.BaseReport.Entities;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportInitializerUnit<in T> where T : Report
{
    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    Task InitializeReportAsync(T report, CancellationToken token);
}