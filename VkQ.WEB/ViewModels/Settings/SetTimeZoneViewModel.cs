using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Settings
{
    public class SetTimeZoneViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Выберите часовой пояс")]
        [StringLength(50, ErrorMessage = "Не больше 50 символов")]
        public string Id { get; set; }
    }
}