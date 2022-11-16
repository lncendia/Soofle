using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Users.Entities;

namespace VkQ.Domain.Services.Services.Factories;

public class LikeReportFactory : IPublicationReportFactory<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;

    public LikeReportFactory(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public LikeReport Create(User user, string hashtag, DateTimeOffset? startDate = null)
    {
        throw new NotImplementedException();
    }
}