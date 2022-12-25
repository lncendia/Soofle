using System.ComponentModel.DataAnnotations.Schema;
using VkQ.Infrastructure.DataStorage.Models.Reports.PublicationReport;

namespace VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeReportElementModel : PublicationReportElementModel
{
    public List<LikeModel> Likes { get; set; } = new();
}