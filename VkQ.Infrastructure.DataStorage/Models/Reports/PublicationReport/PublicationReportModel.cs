using VkQ.Infrastructure.DataStorage.Models.Reports.Base;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

public class PublicationReportModel : ReportModel
{
    public List<UserModel> LinkedUsers { get; set; } = new();
    public string Hashtag { get; set; } = null!;
    public DateTimeOffset? SearchStartDate { get; set; }
    public List<PublicationModel> Publications { get; set; } = new();
}