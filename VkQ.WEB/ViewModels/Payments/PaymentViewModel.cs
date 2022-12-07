namespace VkQ.WEB.ViewModels.Payments;

public class PaymentViewModel
{
    public PaymentViewModel(Guid id, string paymentSystemId, decimal amount, DateTimeOffset creationDate, string payUrl)
    {
        Id = id;
        Amount = amount;
        CreationDate = creationDate;
        PayUrl = payUrl;
        PaymentSystemId = paymentSystemId;
    }

    public Guid Id { get; }
    public string PaymentSystemId { get; }
    public decimal Amount { get; }
    public DateTimeOffset CreationDate { get; }
    public string PayUrl { get; }
}