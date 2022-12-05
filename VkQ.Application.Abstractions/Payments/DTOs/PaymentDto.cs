namespace VkQ.Application.Abstractions.Payments.DTOs;

public class PaymentDto
{
    public PaymentDto(string id, decimal amount, DateTimeOffset date, bool isSuccessful, DateTimeOffset? completionDate, string payUrl)
    {
        if (isSuccessful && completionDate == null)
            throw new ArgumentException("Completion date must be set for successful payments");
        Id = id;
        Amount = amount;
        CreationDate = date;
        IsSuccessful = isSuccessful;
        CompletionDate = completionDate;
        PayUrl = payUrl;
    }

    public string Id { get; }
    public decimal Amount { get; }
    public DateTimeOffset CreationDate { get; }
    public DateTimeOffset? CompletionDate { get; }
    public bool IsSuccessful { get; }
    public string PayUrl { get; }
}