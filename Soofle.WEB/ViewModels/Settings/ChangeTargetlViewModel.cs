using System.ComponentModel.DataAnnotations;

namespace Soofle.WEB.ViewModels.Settings;

public class ChangeTargetViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Введите Id цели")]
    public long? Target { get; set; }
}