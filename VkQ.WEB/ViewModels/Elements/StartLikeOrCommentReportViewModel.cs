using Microsoft.AspNetCore.Mvc.Rendering;

namespace VkQ.WEB.ViewModels.Reports;

public class StartLikeOrCommentReportViewModel
{
    public LikeOrCommentReportAutoViewModel AnalysisAuto { get; set; }
    public LikeOrCommentReportLinksViewModel AnalysisLinks { get; set; }
    public List<SelectListItem> CommonInstagrams { get; set; }
}