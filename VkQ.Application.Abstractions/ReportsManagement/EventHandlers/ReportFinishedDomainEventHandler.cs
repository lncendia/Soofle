using MediatR;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.ReportLogs.Specification;
using VkQ.Domain.Reposts.BaseReport.Events;

namespace VkQ.Application.Abstractions.ReportsManagement.EventHandlers;

public class ReportFinishedDomainEventHandler : INotificationHandler<ReportFinishedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportFinishedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ReportFinishedEvent notification, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.ReportLogRepository.Value.FindAsync(
            new LogByReportIdSpecification(notification.ReportId), null, 0, 1);
        if (!logs.Any()) throw new Exception("Log not found");
        var log = logs.First();
        log.Finish(notification.Success);
        await _unitOfWork.ReportLogRepository.Value.UpdateAsync(log);
    }
}