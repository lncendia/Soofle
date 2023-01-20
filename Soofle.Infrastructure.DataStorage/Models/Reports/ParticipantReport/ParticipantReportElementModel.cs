using Soofle.Infrastructure.DataStorage.Models.Reports.Base;
using Soofle.Domain.Participants.Enums;
using Soofle.Domain.Reposts.ParticipantReport.Enums;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportElementModel : ReportElementModel
{
    public string? NewName { get; set; }
    public Guid? ParticipantId { get; set; }
    public ElementType? Type { get; set; }
    public ParticipantType ParticipantType { get; set; }
    public int? OwnerId { get; set; }
    
    public ParticipantReportModel Report { get; set; } = null!;
}