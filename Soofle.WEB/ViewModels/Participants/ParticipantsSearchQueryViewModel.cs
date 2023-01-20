using System.ComponentModel.DataAnnotations;
using Soofle.Domain.Participants.Enums;

namespace Soofle.WEB.ViewModels.Participants;

public class ParticipantsSearchQueryViewModel
{
    public int Page { get; set; } = 1;
    [StringLength(50)] public string? Username { get; set; }
    public ParticipantType? Type { get; set; }
    public bool? Vip { get; set; }
    public bool? HasChild { get; set; }
}