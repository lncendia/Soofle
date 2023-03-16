using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.ReportsProcessors.Processors;

public class ReportProcessorService : IReportProcessorService
{
    public ReportProcessorService(IUnitOfWork unitOfWork, ILikeInfoService likeInfoService,
        ICommentInfoService commentInfoService, IParticipantsService participantsGetterService)
    {
        LikeReportProcessor = new Lazy<IReportProcessorUnit<LikeReport>>(
            () => new LikeReportProcessor(unitOfWork, likeInfoService));
        CommentReportProcessor = new Lazy<IReportProcessorUnit<CommentReport>>(
            () => new CommentReportProcessor(unitOfWork, commentInfoService));
        ParticipantReportProcessor = new Lazy<IReportProcessorUnit<ParticipantReport>>(
            () => new ParticipantReportProcessor(unitOfWork, participantsGetterService));
    }

    public Lazy<IReportProcessorUnit<LikeReport>> LikeReportProcessor { get; }
    public Lazy<IReportProcessorUnit<CommentReport>> CommentReportProcessor { get; }
    public Lazy<IReportProcessorUnit<ParticipantReport>> ParticipantReportProcessor { get; }
}