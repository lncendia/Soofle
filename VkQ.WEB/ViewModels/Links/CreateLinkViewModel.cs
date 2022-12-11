using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Links;

public class CreateLinkViewModel
{
    [Required(ErrorMessage = "Не указана сумма платежа")]
    [Display(Name = "Сумма оплаты")]
    [Range(1, 10000, ErrorMessage = "Сумма должна быть больше 0 и меньше 10000 рублей")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }
}