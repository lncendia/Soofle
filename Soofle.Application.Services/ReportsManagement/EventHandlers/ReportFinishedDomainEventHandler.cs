using MediatR;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.ReportLogs.Specification;
using Soofle.Domain.Reposts.BaseReport.Events;

namespace Soofle.Application.Services.ReportsManagement.EventHandlers;

public class ReportFinishedDomainEventHandler : INotificationHandler<ReportFinishedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportFinishedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ReportFinishedEvent notification, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.ReportLogRepository.Value.FindAsync(
            new LogByReportIdSpecification(notification.ReportId));
        if (!logs.Any()) throw new ArgumentException("Log not found", nameof(notification));
        foreach (var x in logs)
        {
            x.Finish(notification.Success, notification.FinishedAt);
            await _unitOfWork.ReportLogRepository.Value.UpdateAsync(x);
        }
    }
}