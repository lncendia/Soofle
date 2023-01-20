using Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeReportElementModel : PublicationReportElementModel
{
    public string Likes { get; set; } = null!;
    public Guid ReportId { get; set; }
    public LikeReportModel Report { get; set; } = null!;
}