using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Services.StaticMethods;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Services.Services.Builders;

public class LikeReportBuilder : IPublicationReportBuilder<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public LikeReportBuilder(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<LikeReport> BuildAsync(User user, string hashtag, DateTimeOffset? startDate = null,
        List<Guid>? coAuthors = null)
    {
        await ReportBuilders.CheckUserForCreate(_unitOfWork, user, coAuthors);
        return new LikeReport(user, hashtag, startDate, coAuthors);
    }
}