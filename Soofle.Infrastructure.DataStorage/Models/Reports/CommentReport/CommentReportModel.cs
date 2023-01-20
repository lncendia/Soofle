using Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport;

public class CommentReportModel : PublicationReportModel
{
    public List<CommentReportElementModel> ReportElementsList { get; set; } = new();
}