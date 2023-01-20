using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Domain.Reposts.BaseReport.Entities;
using Soofle.Domain.Reposts.BaseReport.Exceptions;

namespace Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;

public interface IReportProcessorUnit<in T> where T : Report
{
    /// <exception cref="ReportAlreadyCompletedException">Report already completed</exception>
    /// <exception cref="ReportNotStartedException">Report not initialized</exception>
    /// <exception cref="VkIsNotActiveException">Users vk is not active</exception>
    /// <exception cref="ProxyIsNotSetException">Proxy for report creator is not set</exception>
    /// <exception cref="TooManyRequestErrorsException">Too many request errors</exception>
    Task ProcessReportAsync(T report, CancellationToken token);
}