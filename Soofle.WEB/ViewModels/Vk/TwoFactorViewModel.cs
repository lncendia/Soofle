using System.ComponentModel.DataAnnotations;

namespace Soofle.WEB.ViewModels.Vk;

public class TwoFactorViewModel
{
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    [Display(Name = "Код")]
    public string? Code { get; set; }
}