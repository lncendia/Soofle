using VkQ.Domain.Users.Entities;

namespace VkQ.WEB.ViewModels.Settings
{
    public class CommunicationsViewModel
    {
        public CommunicationsViewModel(Guid currentUserId, string scheme)
        {
            CurrentUserId = currentUserId;
            Scheme = scheme;
        }

        public List<CommunicationLink> Links { get; }
        public Guid CurrentUserId { get; }
        public string Scheme { get; }
    }
}