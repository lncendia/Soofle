using Soofle.Infrastructure.DataStorage.Models.Reports.Base;

namespace Soofle.Infrastructure.DataStorage.Models.Reports.ParticipantReport;

public class ParticipantReportModel : ReportModel
{
    public long VkId { get; set; }
    public List<ParticipantReportElementModel> ReportElementsList { get; set; } = new();
}