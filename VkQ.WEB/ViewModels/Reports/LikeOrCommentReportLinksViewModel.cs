using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Reports
{
    public class LikeOrCommentReportLinksViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int Id { get; set; }

        [Display(Name = "Ссылки")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(8500, ErrorMessage = "Не более 8500 символов")]
        public string Links { get; set; }

        [Display(Name = "Тег")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        public string Tag { get; set; }

        [Display(Name = "Проверить только тех участников, которые воспользовались тегом")]
        public bool NotAllParticipants { get; set; }

        [Display(Name = "API")] public Api Api { get; set; }
        [Display(Name = "Публикации")] public Publications Publications { get; set; }

        [Display(Name = "Чаты для совместной проверки")]
        public List<int> CommonAccounts { get; set; }
    }
}