using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportElementModel : ReportElementModel
{
    public string? OldName { get; set; }
    public ElementType Type { get; set; }
}