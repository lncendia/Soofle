using VkQ.Infrastructure.DataStorage.Models.Reports.Base.PublicationReport;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeReportElementModel : PublicationReportElementModel
{
    public List<LikeModel> Likes { get; set; } = new();
}