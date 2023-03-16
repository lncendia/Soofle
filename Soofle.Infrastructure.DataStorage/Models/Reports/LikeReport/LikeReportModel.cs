using Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeReportModel : PublicationReportModel
{
    public List<LikeReportElementModel> ReportElementsList { get; set; } = new();
}