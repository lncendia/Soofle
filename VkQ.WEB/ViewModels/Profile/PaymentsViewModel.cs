namespace VkQ.WEB.ViewModels.Profile;

public class PaymentsViewModel
{
    public PaymentsViewModel(IEnumerable<PaymentViewModel> payments, DateTimeOffset? subscriptionStart,
        DateTimeOffset? subscriptionEnd)
    {
        Payments = payments.ToList();
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
    }

    public DateTimeOffset? SubscriptionStart { get; }
    public DateTimeOffset? SubscriptionEnd { get; }
    public List<PaymentViewModel> Payments { get; }

    public class PaymentViewModel
    {
        public PaymentViewModel(Guid id, decimal amount, DateTimeOffset creationDate, DateTimeOffset? completionDate,
            bool isSuccessful, string payUrl)
        {
            Amount = amount;
            CreationDate = creationDate;
            CompletionDate = completionDate;
            IsSuccessful = isSuccessful;
            PayUrl = isSuccessful ? null : payUrl;
            Id = id;
        }

        public Guid Id { get; }
        public decimal Amount { get; }
        public DateTimeOffset CreationDate { get; }
        public DateTimeOffset? CompletionDate { get; }
        public bool IsSuccessful { get; }
        public string? PayUrl { get; }
    }
}