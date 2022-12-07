using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Vk;

public class TwoFactorViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    [Display(Name = "Код")]
    public string Code { get; set; } = null!;
}