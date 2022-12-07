using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Users;

public class EditUserViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите электронный адрес")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string Email { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Введите электронный адрес")]
    [StringLength(50, ErrorMessage = "Не больше 50 символов")]
    public string? NewEmail { get; set; }
    
    [StringLength(20, ErrorMessage = "Не больше 20 символов")]
    [Display(Name = "Введите имя пользователя")]
    public string? Username { get; set; }

    [Display(Name = "Подписка")]
    [DataType(DataType.DateTime)]
    public DateTime? Subscribe { get; set; }

    [Display(Name = "Админ")] 
    public bool? Admin { get; set; }

    [Display(Name = "Заблокировать до")]
    [DataType(DataType.DateTime)]
    public DateTime? Block { get; set; }
}