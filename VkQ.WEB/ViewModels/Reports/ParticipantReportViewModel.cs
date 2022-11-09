using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Reports
{
    public class ParticipantReportViewModel
    {
        public List<ParticipantReport> ParticipantReports{ get; set; }
        public Report Report { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")] public int Id { get; set; }
        [StringLength(50)] public string Username { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public ParticipantStatus Status { get; set; } = ParticipantStatus.All;
    }
}