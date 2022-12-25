using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VkQ.Infrastructure.DataStorage.Models;

public class TransactionModel : IModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    [Column(TypeName = "varchar(120)")] public string PaymentSystemId { get; set; } = null!;
    [Column(TypeName = "varchar(120)")] public string PaymentSystemUrl { get; set; } = null!;
    [Precision(10, 2)] public decimal Amount { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ConfirmationDate { get; set; }
    public bool IsSuccessful { get; set; }
}