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
        for (; i < report.Publications.Count; i += 3)
        {
            var publications = report.Publications.Skip(i).Take(3).ToList();
            var tasks = publications.Select(x => GetAsync(x, info)).ToList();
            try
            {
                await Task.WhenAll(tasks);
                count = 0;
            }
            catch (Exception exception)
            {
                var exceptions = tasks.Where(x => x.IsFaulted).SelectMany(x => x.Exception!.InnerExceptions).ToList();
                var ex = exceptions.FirstOrDefault(x => x is not VkRequestException);
                if (ex != null) throw ex;
                count += exceptions.Count;
                if (count > 5) throw new TooManyRequestErrorsException(exception.Message);
            }
            finally
            {
                for (int j = 0; j < publications.Count; j++)
                {
                    var pub = publications[j];
                    var result = tasks[j];

                    var likeResult = result.IsFaulted
                        ? new LikesDto(pub.ItemId, pub.OwnerId)
                        : new LikesDto(pub.ItemId, pub.OwnerId, result.Result);
                    report.SetLikes(likeResult);
                }

                await Task.Delay(500, token);
                if (i % 60 == 0) await SaveAsync(report);
            }
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