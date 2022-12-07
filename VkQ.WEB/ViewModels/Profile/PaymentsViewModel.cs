namespace VkQ.WEB.ViewModels.Profile;

public class PaymentsViewModel
{
    public PaymentsViewModel(List<PaymentViewModel> payments, DateTimeOffset? subscriptionStart,
        DateTimeOffset? subscriptionEnd)
    {
        Payments = payments;
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
    }

    public DateTimeOffset? SubscriptionStart { get; }
    public DateTimeOffset? SubscriptionEnd { get; }
    public List<PaymentViewModel> Payments { get; }

    public class PaymentViewModel
    {
        public PaymentViewModel(string id, decimal amount, DateTimeOffset creationDate, DateTimeOffset? completionDate,
            bool isSuccessful, string payUrl)
        {
            Amount = amount;
            CreationDate = creationDate;
            CompletionDate = completionDate;
            IsSuccessful = isSuccessful;
            PayUrl = payUrl;
            Id = id;
        }

        public string Id { get; }
        public decimal Amount { get; }
        public DateTimeOffset CreationDate { get; }
        public DateTimeOffset? CompletionDate { get; }
        public bool IsSuccessful { get; }
        public string PayUrl { get; }
    }
}