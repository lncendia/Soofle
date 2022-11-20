using System.ComponentModel.DataAnnotations;
using VkQ.Domain.Participants.Entities;

namespace VkQ.WEB.ViewModels.Participants
{
    public class EditParticipantViewModel
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")] public int Id { get; set; }

        [Display(Name = "Заметка")]
        [StringLength(100, ErrorMessage = "Не больше 100 символов")]
        public string Note { get; set; }

        [Display(Name = "VIP")] public bool Vip { get; set; }

        [Display(Name = "Родительский аккаунт")]
        public int ParentId { get; set; }
        
        public List<Participant> Participants { get; set; }
    }
}