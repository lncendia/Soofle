using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.AccountsInstagram
{
    public class TwoFactorViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Не больше 50 символов")]
        [Display(Name = "Код")]
        public string Code { get; set; }
    }
}