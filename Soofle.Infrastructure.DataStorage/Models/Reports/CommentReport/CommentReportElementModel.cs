using Soofle.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.CommentReport;

public class CommentReportElementModel : PublicationReportElementModel
{
    public string Comments { get; set; } = null!;
    public Guid ReportId { get; set; }
    public CommentReportModel Report { get; set; } = null!;
}