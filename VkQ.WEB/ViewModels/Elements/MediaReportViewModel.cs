using System.ComponentModel.DataAnnotations;
using VkQ.Domain.Reposts.BaseReport.Entities.Base;

namespace VkQ.WEB.ViewModels.Reports;

public class MediaReportViewModel
{
    public List<MediaReport> MediaReports { get; set; }
    public Report Report { get; set; }
    [Required(ErrorMessage = "Поле не должно быть пустым")] public int Id { get; set; }
    [StringLength(50)] public string Username { get; set; }
    [StringLength(50)] public string LikeChat { get; set; }
    [StringLength(50)] public string Note { get; set; }
    public bool Vip { get; set; }
    [Required(ErrorMessage = "Поле не должно быть пустым")] public LikingCommentingStatus Status { get; set; } = LikingCommentingStatus.All;
    [Required(ErrorMessage = "Поле не должно быть пустым")] public ParticipantHasChild HasChild { get; set; } = ParticipantHasChild.All;
}