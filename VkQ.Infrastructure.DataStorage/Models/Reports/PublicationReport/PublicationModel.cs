namespace VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public class PublicationModel : IModel
{
    public Guid Id { get; set; }
    public long ItemId { get; set; }
    public long OwnerId { get; set; }
    public PublicationReportModel Report { get; set; } = null!;
}