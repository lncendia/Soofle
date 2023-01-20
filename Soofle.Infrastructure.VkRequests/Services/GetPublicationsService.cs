using VkNet.Abstractions;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;

namespace Soofle.Infrastructure.VkRequests.Services;

public class GetPublicationsService : IPublicationService
{
    private readonly ICaptchaSolver _solver;

    public GetPublicationsService(ICaptchaSolver solver) => _solver = solver;


    private static async Task GetNewsAsync(IVkApiCategories api, List<NewsSearchItem> items, string query,
        DateTime? endTimeLocal, int count, string? nextFrom, CancellationToken token)
    {
        if (items.Count >= count) return;
        var countRequest = count - items.Count;
        if (countRequest > 200) countRequest = 200;

        var response = await api.NewsFeed.SearchAsync(new NewsFeedSearchParams
            { EndTime = endTimeLocal, Query = query, Count = countRequest, StartFrom = nextFrom });
        if (!response.Items.Any()) return;
        items.AddRange(response.Items);
        if (string.IsNullOrEmpty(response.NextFrom)) return;
        await Task.Delay(500, token);
        await GetNewsAsync(api, items, query, endTimeLocal, count, response.NextFrom, token);
    }


    public async Task<List<PublicationDto>> GetAsync(RequestInfo data, string hashtag, int count,
        DateTimeOffset? limitTime, CancellationToken token)
    {
        if (count < 1) throw new ArgumentException("Count can't be less then zero.");
        if (string.IsNullOrEmpty(hashtag)) throw new ArgumentException("Hashtag can't be null or empty.");

        var publications = new List<NewsSearchItem>();
        try
        {
            await GetNewsAsync(await VkApi.BuildApiAsync(data, _solver), publications, hashtag, limitTime?.DateTime,
                count, null, token);
            var list = publications.Select(item => new PublicationDto(item.Id, item.FromId)).ToList();
            return list;
        }
        catch (VkApiException ex)
        {
            throw VkApi.GetException(ex);
        }
    }
}