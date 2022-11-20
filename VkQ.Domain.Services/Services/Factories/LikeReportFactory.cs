using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Services.StaticMethods;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Services.Services.Factories;

public class LikeReportFactory : IPublicationReportFactory<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public LikeReportFactory(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<LikeReport> CreateAsync(User user, string hashtag, DateTimeOffset? startDate = null,
        List<Guid>? coAuthors = null)
    {
        await ReportFactories.CheckUserForCreate(_unitOfWork, user, coAuthors);
        return new LikeReport(user, hashtag, startDate, coAuthors);
    }
}