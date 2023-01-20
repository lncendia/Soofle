using MediatR;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Transactions.Events;

namespace Soofle.Application.Services.Payments.EventHandlers;

public class TransactionAcceptedDomainEventHandler : INotificationHandler<TransactionAcceptedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly decimal _amount;

    public TransactionAcceptedDomainEventHandler(IUnitOfWork unitOfWork, decimal amount)
    {
        _unitOfWork = unitOfWork;
        _amount = amount;
    }

    public async Task Handle(TransactionAcceptedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(notification.UserId);
        if (user is null) throw new UserNotFoundException();
        var time = notification.Amount / _amount;
        if (time < 1) throw new ArgumentException("Time is less than 1", nameof(notification));
        user.AddSubscription(TimeSpan.FromDays(Convert.ToDouble(time)));
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
    }
}