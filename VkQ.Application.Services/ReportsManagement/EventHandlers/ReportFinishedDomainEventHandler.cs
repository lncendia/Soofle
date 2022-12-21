using MediatR;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.ReportLogs.Specification;
using VkQ.Domain.Reposts.BaseReport.Events;

namespace VkQ.Application.Services.ReportsManagement.EventHandlers;

public class ReportFinishedDomainEventHandler : INotificationHandler<ReportFinishedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportFinishedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(ReportFinishedEvent notification, CancellationToken cancellationToken)
    {
        var logs = await _unitOfWork.ReportLogRepository.Value.FindAsync(
            new LogByReportIdSpecification(notification.ReportId));
        if (!logs.Any()) throw new Exception("Log not found"); //todo:
        foreach (var x in logs)
        {
            x.Finish(notification.Success);
            await _unitOfWork.ReportLogRepository.Value.UpdateAsync(x);
        }
        await _unitOfWork.SaveChangesAsync();
    }
}