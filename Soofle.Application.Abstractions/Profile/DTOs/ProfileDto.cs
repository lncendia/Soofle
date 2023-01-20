namespace Soofle.Application.Abstractions.Profile.DTOs;

public class ProfileDto
{
    public ProfileDto(VkDto? vk, long? chatId, StatsDto stats, List<LinkDto> links, List<PaymentDto> payments,
        DateTimeOffset? subscriptionStart, DateTimeOffset? subscriptionEnd)
    {
        Links = links;
        Payments = payments;
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
        Stats = stats;
        Vk = vk;
        ChatId = chatId;
    }

    public VkDto? Vk { get; }
    public StatsDto Stats { get; }
    public long? ChatId { get; }

    public List<LinkDto> Links { get; }
    public List<PaymentDto> Payments { get; }
    public DateTimeOffset? SubscriptionStart { get; }
    public DateTimeOffset? SubscriptionEnd { get; }
}