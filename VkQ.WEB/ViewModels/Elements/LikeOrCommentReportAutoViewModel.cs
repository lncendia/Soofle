using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Reports;

public class LikeOrCommentReportAutoViewModel
{
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    public int Id { get; set; }

    [Display(Name = "Тег")]
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Tag { get; set; }

    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [Display(Name = "Количество постов")]
    [Range(10, 120, ErrorMessage = "Количество постов должно быть в диапазоне от 10 до 120")]
    public int Count { get; set; }

    [Display(Name = "Проверить только тех участников, которые воспользовались тегом")]
    public bool NotAllParticipants { get; set; }

    [Display(Name = "API")] public Api Api { get; set; }
    [Display(Name = "Публикации")] public Publications Publications { get; set; }

    [Display(Name = "Чаты для совместной проверки")]
    public List<int> CommonAccounts { get; set; }

    [Display(Name = "Опубликовано с")]
    [DataType(DataType.DateTime)]

    public DateTime? StartDate { get; set; }

    [Display(Name = "Опубликовано до")]
    [DataType(DataType.DateTime)]
    public DateTime? EndDate { get; set; }
}