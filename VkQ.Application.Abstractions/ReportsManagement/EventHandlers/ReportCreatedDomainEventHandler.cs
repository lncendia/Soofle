using MediatR;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.ReportLogs.Entities;
using VkQ.Domain.Reposts.BaseReport.Events;

namespace VkQ.Application.Abstractions.ReportsManagement.EventHandlers;

public class ReportCreatedDomainEventHandler : INotificationHandler<ReportCreatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportCreatedDomainEventHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public Task Handle(ReportCreatedEvent notification, CancellationToken cancellationToken)
    {
        var reportLog = new ReportLog(notification.UserId, notification.ReportId, notification.Type,
            notification.CreatedAt, notification.AdditionalInfo);
        return _unitOfWork.ReportLogRepository.Value.AddAsync(reportLog);
    }
}