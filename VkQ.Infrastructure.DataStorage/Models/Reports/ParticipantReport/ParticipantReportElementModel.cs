using VkQ.Domain.Reposts.ParticipantReport.Enums;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportElementModel
{
    public string? OldName { get; set; }
    public ElementType Type { get; set; }
}