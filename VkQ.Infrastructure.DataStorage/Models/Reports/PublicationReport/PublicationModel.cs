using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public class PublicationModel : IModel
{
    public Guid Id { get; set; }
    public long ItemId { get; set; }
    public long OwnerId { get; set; }
}