using MediatR;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Transactions.Events;

namespace VkQ.Application.Services.Payments.EventHandlers;

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
        if (time < 1) throw new Exception("Time is less than 1");
        user.AddSubscription(TimeSpan.FromDays(Convert.ToDouble(time)));
        await _unitOfWork.UserRepository.Value.UpdateAsync(user);
    }
}