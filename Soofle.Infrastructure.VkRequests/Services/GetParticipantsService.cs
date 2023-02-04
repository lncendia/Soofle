using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Domain.Participants.Enums;

namespace Soofle.Infrastructure.VkRequests.Services;

public class GetParticipantsService : IParticipantsService
{
    private readonly ICaptchaSolver _solver;

    public GetParticipantsService(ICaptchaSolver solver) => _solver = solver;

    private static async Task<IEnumerable<ParticipantDto>> GetFriendsAsync(IVkApiCategories api, long id, int count)
    {
        var friends = await api.Friends.GetAsync(new FriendsGetParams
        {
            UserId = id, Count = count, Fields = ProfileFields.Domain
        });
        return friends.Select(GetParticipant);
    }

    private static async Task<IEnumerable<ParticipantDto>> GetSubscriptionsAsync(IVkApiInvoke api, long id, int count)
    {
        var parameters = new VkParameters
        {
            {
                "user_id", id
            },
            {
                "extended", true
            },
            {
                "offset", 0
            },
            {
                "count", count
            },
            {
                "fields", ProfileFields.Domain
            }
        };

        var response = await api.CallAsync("users.getSubscriptions", parameters);
        return response.ToVkCollectionOf(Models.Subscription.FromJson).Select(GetParticipant);
    }

    private static ParticipantDto GetParticipant(Models.Subscription subscription)
    {
        string name;
        ParticipantType type;
        if (subscription.Type == GroupType.Page || subscription.Type == GroupType.Group || subscription.Type == GroupType.Event)
        {
            name = subscription.ScreenName;
            type = ParticipantType.Group;
        }
        else if (subscription.Type == SafetyEnum<GroupType>.RegisterPossibleValue("profile"))
        {
            name = subscription.Domain;
            type = ParticipantType.User;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(subscription), subscription.Type, null);
        }

        return new ParticipantDto(subscription.Id, name, type);
    }

    private static ParticipantDto GetParticipant(User user) =>
        new(user.Id, user.Domain, ParticipantType.User);

    public async Task<List<ParticipantDto>> GetFriendsAsync(RequestInfo data, long id, int count, CancellationToken token)
    {
        var api = await VkApi.BuildApiAsync(data, _solver);
        try
        {
            var friends = await GetFriendsAsync(api, id, count);
            return friends.ToList();
        }
        catch (VkApiMethodInvokeException ex)
        {
            throw VkApi.GetException(ex);
        }
    }
    
    public async Task<List<ParticipantDto>> GetSubscriptionsAsync(RequestInfo data, long id, int count, CancellationToken token)
    {
        var api = await VkApi.BuildApiAsync(data, _solver);
        try
        {
            var subscriptions = await GetSubscriptionsAsync(api, id, count);
            return subscriptions.ToList();
        }
        catch (VkApiMethodInvokeException ex)
        {
            throw VkApi.GetException(ex);
        }
    }
}