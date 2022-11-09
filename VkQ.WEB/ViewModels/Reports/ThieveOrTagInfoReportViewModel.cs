using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Reports
{
    public class ThieveOrTagInfoReportViewModel
    {
        public List<ParticipantReport> Reports { get; set; }
        public Report Report { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")] public int Id { get; set; }
        [StringLength(50)] public string Username { get; set; }
        [StringLength(50)] public string LikeChat { get; set; }
        [StringLength(50)] public string Note { get; set; }
        public TagInfoStatus Status { get; set; } = TagInfoStatus.All;
    }
}