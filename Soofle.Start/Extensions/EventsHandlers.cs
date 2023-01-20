using MediatR;
using Soofle.Application.Services.Payments.EventHandlers;
using Soofle.Application.Services.ReportsManagement.EventHandlers;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.BaseReport.Events;
using Soofle.Domain.Reposts.ParticipantReport.Events;
using Soofle.Domain.Transactions.Events;

namespace Soofle.Start.Extensions;

internal static class EventsHandlers
{
    internal static void AddEventsHandlers(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Program).Assembly);
        services.AddTransient<INotificationHandler<TransactionAcceptedEvent>, TransactionAcceptedDomainEventHandler>(
            x => new TransactionAcceptedDomainEventHandler(x.GetRequiredService<IUnitOfWork>(), 10));
        services
            .AddTransient<INotificationHandler<ParticipantReportFinishedEvent>,
                ParticipantReportFinishedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportCreatedEvent>, ReportCreatedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportFinishedEvent>, ReportFinishedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportDeletedEvent>, ReportDeletedDomainEventHandler>();
    }
}