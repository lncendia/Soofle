using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Application.Services.ReportsProcessors.StaticMethods;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.CommentReport.Entities;

namespace Soofle.Application.Services.ReportsProcessors.Initializers;

public class CommentReportInitializer : IReportInitializerUnit<CommentReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationService _publicationGetterService;

    public CommentReportInitializer(IUnitOfWork unitOfWork, IPublicationService publicationGetterService)
    {
        _unitOfWork = unitOfWork;
        _publicationGetterService = publicationGetterService;
    }

    ///<exception cref="LinkedUserNotFoundException">User in coauthors list not found</exception>
    public async Task InitializeReportAsync(CommentReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        var t1 = ReportInitializer.GetPublicationsAsync(report, info, _publicationGetterService, token);
        var t2 = ReportInitializer.GetParticipantsAsync(report, _unitOfWork);
        await Task.WhenAll(t1, t2);
        report.Start(t2.Result, t1.Result);
    }
}