using System.ComponentModel.DataAnnotations;

namespace Soofle.WEB.ViewModels.Vk;

public class SetVkViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Логин")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string Login { get; set; } = null!;
    
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;
}