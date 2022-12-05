using VkQ.Domain.Participants.Enums;
using VkQ.Domain.Reposts.ParticipantReport.Enums;
using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportElementModel : ReportElementModel
{
    public string? NewName { get; set; }
    public Guid? ParticipantId { get; set; }
    public ElementType? Type { get; set; }
    public ParticipantType ParticipantType { get; set; }
}