using VkQ.Domain.Abstractions.DTOs;
using VkQ.Domain.Abstractions.Interfaces;
using VkQ.Domain.Abstractions.Services;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Publication;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Services.StaticMethods;
using LikeDto = VkQ.Domain.Reposts.LikeReport.DTOs;

namespace VkQ.Domain.Services.Services.Processors;

public class LikeReportProcessorService : IReportProcessorService<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationInfoService _publicationInfoService;

    public LikeReportProcessorService(IUnitOfWork unitOfWork, IPublicationInfoService publicationInfoService)
    {
        _unitOfWork = unitOfWork;
        _publicationInfoService = publicationInfoService;
    }

    public async Task ProcessReportAsync(LikeReport report, CancellationToken token)
    {
        if (!report.IsInitialized) throw new ReportNotInitializedException();
        if (!report.IsStarted) throw new ReportNotStartedException();
        if (report.IsCompleted) throw new ReportAlreadyCompletedException();
        
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        if (report.IsCompleted) throw new Exception();
        await ProcessPublicationsAsync(report, info, token);
        report.Finish();
        await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task ProcessPublicationsAsync(LikeReport report, RequestInfo info, CancellationToken token)
    {
        var allPublications = report.Publications!;
        var elements = report.Elements!;
        for (var i = elements.First().Likes.Count; i < allPublications.Count; i += 10)
        {
            var publications = allPublications.Skip(i * 10).Take(10).Select(x => (x.ItemId, x.OwnerId)).ToList();
            try
            {
                var likes = await _publicationInfoService.GetLikesAsync(info, publications, token);
                foreach (var like in likes)
                {
                    foreach (var element in elements)
                    {
                        var isLike = like.SuccessLoaded && like.Likes!.Any(x => x == element.VkId);
                        report.SetLike(element.Id,
                            new LikeDto.LikeDto(like.PublicationId, like.OwnerId, isLike, like.SuccessLoaded));
                    }
                }
            }
            catch (Exception e)
            {
                //todo: custom exceptions
                foreach (var pub in publications)
                {
                    foreach (var element in elements)
                    {
                        report.SetLike(element.Id, new LikeDto.LikeDto(pub.ItemId, pub.OwnerId, false, false));
                    }
                }
            }

            await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}