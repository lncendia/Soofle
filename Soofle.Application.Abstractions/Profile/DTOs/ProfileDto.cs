namespace Soofle.Application.Abstractions.Profile.DTOs;

public class ProfileDto
{
    public ProfileDto(string? vk, long? chatId, DateTimeOffset? subscriptionStart, DateTimeOffset? subscriptionEnd)
    {
        SubscriptionStart = subscriptionStart;
        SubscriptionEnd = subscriptionEnd;
        ChatId = chatId;
        VkLogin = vk;
    }

    public string? VkLogin { get; }
    public long? ChatId { get; }
    
    public DateTimeOffset? SubscriptionStart { get; }
    public DateTimeOffset? SubscriptionEnd { get; }
}