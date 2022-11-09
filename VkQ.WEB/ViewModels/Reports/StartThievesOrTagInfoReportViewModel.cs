using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Reports
{
    public class StartThievesOrTagInfoReportViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")] public int Id { get; set; }

        [Display(Name = "Тег")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        public string Tag { get; set; }
        [Display(Name = "Публикации")] public Publications Publications { get; set; }

        [Display(Name = "Проверить после запуска")]
        public bool CheckAfterStart { get; set; } = true;
    }
}