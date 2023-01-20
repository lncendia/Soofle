using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.ReportsProcessors.Initializers;

public class ReportInitializerService : IReportInitializerService
{
    public ReportInitializerService(IPublicationService publicationGetterService, IUnitOfWork unitOfWork)
    {
        LikeReportInitializer = new Lazy<IReportInitializerUnit<LikeReport>>(() =>
            new LikeReportInitializer(unitOfWork, publicationGetterService));
        CommentReportInitializer = new Lazy<IReportInitializerUnit<CommentReport>>(() =>
            new CommentReportInitializer(unitOfWork, publicationGetterService));
        ParticipantReportInitializer =
            new Lazy<IReportInitializerUnit<ParticipantReport>>(() => new ParticipantReportInitializer(unitOfWork));
    }

    public Lazy<IReportInitializerUnit<LikeReport>> LikeReportInitializer { get; }

    public Lazy<IReportInitializerUnit<ParticipantReport>> ParticipantReportInitializer { get; }
    public Lazy<IReportInitializerUnit<CommentReport>> CommentReportInitializer { get; }
}