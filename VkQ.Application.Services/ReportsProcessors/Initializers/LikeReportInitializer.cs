using VkQ.Application.Abstractions.ReportsProcessors.Exceptions;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Services.ReportsProcessors.StaticMethods;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;

namespace VkQ.Application.Services.ReportsProcessors.Initializers;

public class LikeReportInitializer : IReportInitializerUnit<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationGetterService _publicationGetterService;

    public LikeReportInitializer(IUnitOfWork unitOfWork, IPublicationGetterService publicationGetterService)
    {
        _unitOfWork = unitOfWork;
        _publicationGetterService = publicationGetterService;
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public async Task InitializeReportAsync(LikeReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        var t1 = ReportInitializer.GetPublicationsAsync(report, info, _publicationGetterService, token);
        var t2 = ReportInitializer.GetParticipantsAsync(report, _unitOfWork);
        await Task.WhenAll(t1, t2);
        report.Start(t2.Result, t1.Result);
    }
}