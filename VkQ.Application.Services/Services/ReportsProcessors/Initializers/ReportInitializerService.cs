using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.ReportsProcessors.Initializers;

public class ReportInitializerService : IReportInitializerService
{
    public ReportInitializerService(IPublicationGetterService publicationGetterService, IUnitOfWork unitOfWork)
    {
        LikeReportInitializer = new Lazy<IReportInitializerUnit<LikeReport>>(() =>
            new LikeReportInitializer(unitOfWork, publicationGetterService));

        ParticipantReportInitializer =
            new Lazy<IReportInitializerUnit<ParticipantReport>>(() => new ParticipantReportInitializer(unitOfWork));
    }

    public Lazy<IReportInitializerUnit<LikeReport>> LikeReportInitializer { get; }

    public Lazy<IReportInitializerUnit<ParticipantReport>> ParticipantReportInitializer { get; }
}