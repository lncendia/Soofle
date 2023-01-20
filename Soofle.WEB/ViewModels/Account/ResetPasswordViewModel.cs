using System.ComponentModel.DataAnnotations;

namespace Soofle.WEB.ViewModels.Account;

public class ResetPasswordViewModel
{
    [StringLength(400, ErrorMessage = "Не больше 400 символов")]
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите электронный адрес, к которому привязан аккаунт.")]
    public string Email { get; set; } = null!;
}