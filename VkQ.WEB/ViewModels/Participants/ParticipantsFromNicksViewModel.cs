using System.ComponentModel.DataAnnotations;

namespace VkQ.WEB.ViewModels.Participants
{
    public class ParticipantsFromNicksViewModel
    {
        public List<Participant> Participants { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int Id { get; set; }

        [Display(Name = "Ники")]
        [StringLength(8500, ErrorMessage = "Не более 8500 символов")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Links { get; set; }

        public ParticipantFromNicks Type { get; set; } = ParticipantFromNicks.MyParticipants;
    }
}