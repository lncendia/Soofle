using VkQ.Domain.Users.Entities;

namespace VkQ.WEB.ViewModels.Settings
{
    public class CommunicationsViewModel
    {
        public List<CommunicationLink> Links { get; set; }
        public User CurrentUser { get; set; }
        public string Scheme { get; set; }
    }
}