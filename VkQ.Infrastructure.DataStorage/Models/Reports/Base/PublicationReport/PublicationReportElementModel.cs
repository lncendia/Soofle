namespace VkQ.Infrastructure.DataStorage.Models.Reports.Base.PublicationReport;

public abstract class PublicationReportElementModel : ReportElementModel
{
    public string LikeChatName { get; set; } = null!;
    public Guid ParticipantId { get; set; }
    public bool IsAccepted { get; set; }
}