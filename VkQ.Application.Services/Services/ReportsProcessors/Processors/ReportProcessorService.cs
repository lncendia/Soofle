using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.ReportsProcessors.Processors;

public class ReportProcessorService : IReportProcessorService
{
    public ReportProcessorService(IUnitOfWork unitOfWork, IPublicationInfoService publicationInfoService,
        IParticipantsGetterService participantsGetterService)
    {
        LikeReportProcessor = new Lazy<IReportProcessorUnit<LikeReport>>(
            () => new LikeReportProcessor(unitOfWork, publicationInfoService));
        
        ParticipantReportProcessor = new Lazy<IReportProcessorUnit<ParticipantReport>>(
            () => new ParticipantReportProcessor(unitOfWork, participantsGetterService));
    }

    public Lazy<IReportProcessorUnit<LikeReport>> LikeReportProcessor { get; }
    public Lazy<IReportProcessorUnit<ParticipantReport>> ParticipantReportProcessor { get; }
}