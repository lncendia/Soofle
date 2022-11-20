using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

namespace VkQ.Domain.Abstractions.Interfaces;

public interface IReportProcessorService<in T> where T : Report
{
    ///<exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    ///<exception cref="ReportNotStartedException">Report not initialized</exception>
    ///<exception cref="VkIsNotActiveException">Users vk is not active</exception>
    ///<exception cref="ProxyIsNotSetException">Proxy for report creator is not set</exception>
    Task ProcessReportAsync(T report, CancellationToken token);
}