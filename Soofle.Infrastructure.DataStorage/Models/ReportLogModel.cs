using Soofle.Infrastructure.DataStorage.Models.Abstractions;
using Soofle.Domain.ReportLogs.Enums;

namespace Soofle.Infrastructure.DataStorage.Models;

public class ReportLogModel : IAggregateModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public Guid? ReportId { get; set; }
    public ReportType Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public bool? Success { get; set; }
    public string AdditionalInfo { get; set; } = null!;
}