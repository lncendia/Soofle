using System.ComponentModel.DataAnnotations;
using VkQ.Domain.Participants.Enums;

namespace VkQ.WEB.ViewModels.Participants;

public class ParticipantsSearchQueryViewModel
{
    public int Page { get; set; } = 1;
    [StringLength(50)] public string? Username { get; set; }
    public ParticipantType? Type { get; set; }
    public bool? Vip { get; set; }
    public bool? HasChild { get; set; }
}

// public enum ParticipantType
// {
//     [Display(Name = "Все")] All,
//     [Display(Name = "Пользователь")] User,
//     [Display(Name = "Группа")] Group
// }
//
// public enum VipType
// {
//     [Display(Name = "Все")] All,
//     [Display(Name = "Да")] Yes,
//     [Display(Name = "Нет")] No
// }
//
// public enum ChildType
// {
//     [Display(Name = "Все")] All,
//     [Display(Name = "Да")] Yes,
//     [Display(Name = "Нет")] No
// }