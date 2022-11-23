using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public class PublicationReportElementModel : ReportElementModel
{
    public string LikeChatName { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public bool IsAccepted { get; set; }
}