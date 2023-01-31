using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.Exceptions;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Application.Services.ReportsProcessors.StaticMethods;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.PublicationReport.Entities;
using LikeDto = Soofle.Domain.Reposts.LikeReport.DTOs;
using LikesDto = Soofle.Domain.Reposts.LikeReport.DTOs.LikesDto;

namespace Soofle.Application.Services.ReportsProcessors.Processors;

public class LikeReportProcessor : IReportProcessorUnit<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILikeInfoService _publicationInfoService;

    public LikeReportProcessor(IUnitOfWork unitOfWork, ILikeInfoService publicationInfoService)
    {
        _unitOfWork = unitOfWork;
        _publicationInfoService = publicationInfoService;
    }

    public async Task ProcessReportAsync(LikeReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        await ProcessPublicationsAsync(report, info, token);
    }

    private async Task ProcessPublicationsAsync(LikeReport report, RequestInfo info, CancellationToken token)
    {
        int count = 0, i = report.Process;
        for (; i < report.Publications.Count; i++)
        {
            var publication = report.Publications.ElementAt(i);
            try
            {
                var result = await GetAsync(publication, info);
                report.SetLikes(new LikesDto(publication.ItemId, publication.OwnerId, result));
                count = 0;
            }
            catch (VkRequestException exception)
            {
                count++;
                report.SetLikes(new LikesDto(publication.ItemId, publication.OwnerId));
                if (count > 5) throw new TooManyRequestErrorsException(exception.Message);
            }

            if (i % 60 == 0) await SaveAsync(report);
            await Task.Delay(1000, token);
        }

        await SaveAsync(report);
    }

    private async Task SaveAsync(LikeReport report)
    {
        await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private Task<List<long>> GetAsync(Publication publication, RequestInfo info) =>
        _publicationInfoService.GetAsync(info, publication.ItemId, publication.OwnerId, 500);
}