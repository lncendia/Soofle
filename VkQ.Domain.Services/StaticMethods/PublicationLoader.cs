using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Reposts.BaseReport.Entities;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using PublicationDto = VkQ.Domain.Reposts.BaseReport.DTOs.PublicationDto;

namespace VkQ.Domain.Services.StaticMethods;

public static class PublicationLoader
{
    public static async Task LoadPublicationsAsync(PublicationReport report, RequestInfo info, IPublicationGetterService publicationGetterService, CancellationToken token)
    {
        var publications =
            await publicationGetterService.GetPublicationsAsync(info, report.Hashtag, 1000, report.SearchStartDate, token);
        report.LoadPublications(publications.Select(x => new PublicationDto(x.PublicationId, x.OwnerId)));
    }
}