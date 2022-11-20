using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Participants.Enums;

namespace VkQ.Infrastructure.Publications.Services;

public class GetParticipantsService : IParticipantsGetterService
{
    private readonly ICaptchaSolver _solver;

    public GetParticipantsService(ICaptchaSolver solver) => _solver = solver;

    private static async Task<IEnumerable<ParticipantDto>> GetFriendsAsync(IVkApiCategories api, long id)
    {
        var friends = await api.Friends.GetAsync(new FriendsGetParams
        {
            UserId = id, Count = 1000, Fields = ProfileFields.FirstName | ProfileFields.Uid | ProfileFields.LastName
        });
        return friends.Select(GetParticipant);
    }

    private static async Task<IEnumerable<ParticipantDto>> GetSubscriptionsAsync(IVkApiInvoke api, long id)
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
                "count", 1000
            },
            {
                "fields", "0"
            }
        };

        var response = await api.CallAsync("users.getSubscriptions", parameters);
        return response.ToVkCollectionOf(Models.Subscription.FromJson)
            .Where(s => s.Type == GroupType.Group || s.Type == GroupType.Page)
            .Where(x => x.Deactivated == Deactivated.Activated).Select(GetParticipant);
    }

    private static ParticipantDto GetParticipant(Models.Subscription subscription)
    {
        string name;
        ParticipantType type;
        if (subscription.Type == GroupType.Group)
        {
            name = subscription.Name;
            type = ParticipantType.Group;
        }
        else if (subscription.Type == GroupType.Page)
        {
            name = subscription.FirstName + ' ' + subscription.LastName;
            type = ParticipantType.User;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(subscription.Type), subscription.Type, null);
        }

        return new ParticipantDto(subscription.Id, name, type);
    }

    private static ParticipantDto GetParticipant(User user) =>
        new(user.Id, user.FirstName + ' ' + user.LastName, ParticipantType.User);

    public Task<List<ParticipantDto>> GetParticipantsAsync(RequestInfo data, long id, CancellationToken token)
    {
        //VkApiMethodInvokeException 30
        throw new NotImplementedException();
    }
}