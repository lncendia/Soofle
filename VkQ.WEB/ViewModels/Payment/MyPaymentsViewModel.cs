using VkQ.Domain.Users.Entities;

namespace VkQ.WEB.ViewModels.Payment
{
    public class MyPaymentsViewModel
    {
        public User User { get; set; }
        public int Page { get; set; }
        public List<Models.Payment> Payments { get; set; }
        public List<Models.Rate> Rates { get; set; }
        public int Count { get; set; }
    }
}