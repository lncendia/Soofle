using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public abstract class PublicationReportElementModel : ReportElementModel
{
    public string LikeChatName { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public bool IsAccepted { get; set; }
    public bool Vip { get; set; }
    public Guid? OwnerId { get; set; }
    public PublicationReportElementModel? Owner { get; set; }
}