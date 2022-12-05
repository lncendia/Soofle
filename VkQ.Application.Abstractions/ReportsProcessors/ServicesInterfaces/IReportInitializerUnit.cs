using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportInitializerUnit<in T> where T : Report
{
    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    Task InitializeReportAsync(T report, CancellationToken token);
}