using VkQ.Domain.ReportLogs.Enums;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models;

public class ReportLogModel : IModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public Guid ReportId { get; set; }
    public ReportModel Report { get; set; } = null!;
    public ReportType Type { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public bool? Success { get; set; }
    public string AdditionalInfo { get; set; } = null!;
}