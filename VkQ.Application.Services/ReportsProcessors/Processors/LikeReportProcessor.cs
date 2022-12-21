using VkQ.Application.Abstractions.ReportsProcessors.DTOs;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Services.ReportsProcessors.StaticMethods;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.LikeReport.Entities;
using LikeDto = VkQ.Domain.Reposts.LikeReport.DTOs;

namespace VkQ.Application.Services.ReportsProcessors.Processors;

public class LikeReportProcessor : IReportProcessorUnit<LikeReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublicationInfoService _publicationInfoService;

    public LikeReportProcessor(IUnitOfWork unitOfWork, IPublicationInfoService publicationInfoService)
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
        var allPublications = report.Publications;
        for (var i = report.Process; i < allPublications.Count; i += 10)
        {
            var publications = allPublications.Skip(i * 10).Take(10).Select(x => (x.ItemId, x.OwnerId)).ToList();
            try
            {
                var likes = await _publicationInfoService.GetLikesAsync(info, publications, token);
                likes.ForEach(x => report.SetLikes(new LikeDto.LikesDto(x.PublicationId, x.OwnerId, x.Likes!)));
            }
            catch (Exception e)
            {
                //todo: error handling
                publications.ForEach(x => report.SetLikes(new LikeDto.LikesDto(x.ItemId, x.OwnerId)));
            }

            await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}