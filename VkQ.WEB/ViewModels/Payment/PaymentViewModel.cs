namespace VkQ.WEB.ViewModels.Payment;

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