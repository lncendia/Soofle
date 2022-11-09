using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Users
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Id { get; set; }
        
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Не больше 50 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string Password { get; set; }
    }
}