namespace VkQ.WEB.ViewModels.Profile;

public class ProfileViewModel
{
    public ProfileViewModel(string email, string name, LinksViewModel links, PaymentsViewModel payments, int participantsCount, int reportsCount, int lastMonthReportsCount)
    {
        Email = email;
        Name = name;
        Links = links;
        Payments = payments;
        ParticipantsCount = participantsCount;
        ReportsCount = reportsCount;
        LastMonthReportsCount = lastMonthReportsCount;
    }

    public string Email { get; }
    public string Name { get; }
    public int ParticipantsCount { get; }
    public int ReportsCount { get; }
    public int LastMonthReportsCount { get; }

    public LinksViewModel Links { get; }
    public PaymentsViewModel Payments { get; }
}