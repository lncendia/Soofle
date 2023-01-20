using Soofle.Infrastructure.DataStorage.Models.Abstractions;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public class PublicationModel : IEntityModel
{
    public int Id { get; set; }
    public int EntityId { get; set; }
    public long ItemId { get; set; }
    public long OwnerId { get; set; }
    public bool? IsLoaded { get; set; }
    public PublicationReportModel Report { get; set; } = null!;
}