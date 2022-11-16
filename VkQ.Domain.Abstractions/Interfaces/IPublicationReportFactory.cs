using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Abstractions.Interfaces;

public interface IPublicationReportFactory<out T> where T : PublicationReport
{
    public T Create(User user, string hashtag, DateTimeOffset? startDate = null, List<Guid>? coAuthors = null);
}