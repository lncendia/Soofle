namespace VkQ.Application.Abstractions.Users.DTOs;

public class ProfileDto
{
    public ProfileDto(List<LinkDto> links, List<PaymentDto> payments, int participantsCount,
        int reportsCount, int reportsThisMonthCount, DateTimeOffset? subscriptionStart, DateTimeOffset? subscriptionEnd)
    {
        Links = links;
        Payments = payments;
        ParticipantsCount = participantsCount;
        ReportsCount = reportsCount;
        ReportsThisMonthCount = reportsThisMonthCount;
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
    }

    public List<LinkDto> Links { get; }
    public List<PaymentDto> Payments { get; }
    public int ParticipantsCount { get; }
    public int ReportsCount { get; }
    public int ReportsThisMonthCount { get; }
    public DateTimeOffset? SubscriptionStart { get; }
    public DateTimeOffset? SubscriptionEnd { get; }
}