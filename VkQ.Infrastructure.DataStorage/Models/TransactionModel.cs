namespace VkQ.Infrastructure.DataStorage.Models;

public class TransactionModel : IModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public string PaymentSystemId { get; set; } = null!;
    public string PaymentSystemUrl { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ConfirmationDate { get; set; }
    public bool IsSuccessful { get; set; }
}