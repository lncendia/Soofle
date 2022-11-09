using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Settings
{
    public class CreateCommunicationLinkViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Общие воры")]
        public bool CommonThieves { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Совместные проверки")]
        public bool CommonReports { get; set; }
    }
}