namespace VkQ.WEB.ViewModels.Profile;

public class ProfileViewModel
{
    public ProfileViewModel(string email, string name, LinksViewModel links, PaymentsViewModel payments, StatsViewModel stats, long? likeChat, VkViewModel? vk)
    {
        Email = email;
        Name = name;
        Links = links;
        Payments = payments;
        Stats = stats;
        LikeChat = likeChat;
        Vk = vk;
    }

    public string Email { get; }
    public string Name { get; }
    public long? LikeChat { get; }

    public LinksViewModel Links { get; }
    public PaymentsViewModel Payments { get; }
    public StatsViewModel Stats { get; }
    public VkViewModel? Vk { get; }
}