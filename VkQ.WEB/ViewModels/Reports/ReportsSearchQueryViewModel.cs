using VkQ.Domain.ReportLogs.Enums;

namespace VkQ.WEB.ViewModels.Reports;

public class ReportsSearchQueryViewModel
{
    public int Page { get; set; } = 1;
    public ReportType? ReportType { get; set; }
    public string? Hashtag { get; set; }
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
}