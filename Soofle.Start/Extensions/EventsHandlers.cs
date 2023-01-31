﻿using MediatR;
using Soofle.Application.Services.Payments.EventHandlers;
using Soofle.Application.Services.ReportsManagement.EventHandlers;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.BaseReport.Events;
using Soofle.Domain.Reposts.ParticipantReport.Events;
using Soofle.Domain.Transactions.Events;

namespace Soofle.Start.Extensions;

internal static class EventsHandlers
{
    internal static void AddEventsHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(Program).Assembly);
        var amount = configuration.GetValue<decimal>("Payments:SubscribeCost");
        services.AddTransient<INotificationHandler<TransactionAcceptedEvent>, TransactionAcceptedDomainEventHandler>(
            x =>
            {
                var uow = x.GetRequiredService<IUnitOfWork>();
                return new TransactionAcceptedDomainEventHandler(uow, amount);
            });
        services
            .AddTransient<INotificationHandler<ParticipantReportFinishedEvent>,
                ParticipantReportFinishedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportCreatedEvent>, ReportCreatedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportFinishedEvent>, ReportFinishedDomainEventHandler>();
        services.AddTransient<INotificationHandler<ReportDeletedEvent>, ReportDeletedDomainEventHandler>();
    }
}