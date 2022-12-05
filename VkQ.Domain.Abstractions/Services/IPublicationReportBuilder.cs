using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Abstractions.Services;

public interface IPublicationReportBuilder<T> where T : PublicationReport
{
    public Task<T> BuildAsync(User user, string hashtag, DateTimeOffset? startDate = null,
        List<Guid>? coAuthors = null);
}