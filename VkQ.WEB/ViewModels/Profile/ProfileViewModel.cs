namespace VkQ.WEB.ViewModels.Profile;

public class ProfileViewModel
{
    public ProfileViewModel(string email, string name)
    {
        Email = email;
        Name = name;
    }

    public string Email { get; }
    public string Name { get; }

    public LinksViewModel Links { get; }
    public PaymentsViewModel Payments { get; }
}