using VkQ.Domain.Abstractions.Exceptions;
using VkQ.Domain.Reposts.BaseReport.Entities.Publication;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Abstractions.Interfaces;

public interface IPublicationReportFactory<T> where T : PublicationReport
{
    ///<exception cref="LinkNotFoundException">Some coauthors aren't found</exception>
    ///<exception cref="TooManyReportsException">User has create more then 25 reports for this day</exception>
    public Task<T> CreateAsync(User user, string hashtag, DateTimeOffset? startDate = null,
        List<Guid>? coAuthors = null);
}