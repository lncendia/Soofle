using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Participants
{
    public class ParticipantsViewModel
    {
        public List<Participant> Participants { get; set; }
        public int Count { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int Id { get; set; }

        public int Page { get; set; } = 1;
        [StringLength(50)] public string Username { get; set; }
        [StringLength(50)] public string Note { get; set; }

        public bool Vip { get; set; }
        public ParticipantHasChild HasChild { get; set; } = ParticipantHasChild.All;
    }
}