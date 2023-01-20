namespace Soofle.WEB.ViewModels.Profile;

public class StatsViewModel
{
    public StatsViewModel(int participantsCount, int reportsCount, int lastMonthReportsCount, DateTimeOffset? subscriptionStart, DateTimeOffset? subscriptionEnd)
    {
        ParticipantsCount = participantsCount;
        ReportsCount = reportsCount;
        LastMonthReportsCount = lastMonthReportsCount;
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
    }

    public int ParticipantsCount { get; }
    public int ReportsCount { get; }
    public int LastMonthReportsCount { get; }
    public DateTimeOffset? SubscriptionStart { get; }
    public DateTimeOffset? SubscriptionEnd { get; }
}