using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Rate
{
    public class CreateRateViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Range(1, 60000, ErrorMessage = "Стоимость должна быть в диапазоне от 1 до 60 000 рублей")]
        [Display(Name = "Стоимость")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Длительность")]
        [Range(1, 10000, ErrorMessage = "Длительность должна быть в диапазоне от 1 до 10 000 дней")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Количество постов")]
        [Range(1, 9000000000000000000, ErrorMessage = "Количество постов должно быть в диапазоне от 1 до 9 000 000 000 000 000 000 публикаций")]
        [DataType(DataType.Duration)]
        public long CountPosts { get; set; }

        public List<Models.Rate> Rates { get; set; }
    }
}