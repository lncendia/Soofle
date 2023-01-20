namespace Soofle.WEB.ViewModels.Profile;

public class ProfileViewModel
{
    public ProfileViewModel(string email, string name,  StatsViewModel stats, long? likeChat, VkViewModel? vk, IEnumerable<LinkViewModel> links, IEnumerable<PaymentViewModel> payments)
    {
        Email = email;
        Name = name;
        Stats = stats;
        LikeChat = likeChat;
        Vk = vk;
        Links = links;
        Payments = payments;
    }

    public string Email { get; }
    public string Name { get; }
    public long? LikeChat { get; }

    public IEnumerable<LinkViewModel> Links { get; }
    public IEnumerable<PaymentViewModel> Payments { get; }
    public StatsViewModel Stats { get; }
    public VkViewModel? Vk { get; }
}