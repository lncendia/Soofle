namespace VkQ.Infrastructure.DataStorage.Models.Reports.LikeReport;

public class LikeModel
{
    public int Id { get; set; }
    public Guid PublicationId { get; set; }
    public bool IsLiked { get; set; }
    public bool IsLoaded { get; set; }
    public Guid LikeReportElementId { get; set; }
    public LikeReportElementModel LikeReportElement { get; set; } = null!;
}