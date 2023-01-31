using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.VkRequests.DTOs;
using Soofle.Application.Abstractions.VkRequests.Exceptions;
using Soofle.Application.Abstractions.VkRequests.ServicesInterfaces;
using Soofle.Application.Services.ReportsProcessors.StaticMethods;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.PublicationReport.Entities;
using CommentsDto = Soofle.Domain.Reposts.CommentReport.DTOs.CommentsDto;

namespace Soofle.Application.Services.ReportsProcessors.Processors;

public class CommentReportProcessor : IReportProcessorUnit<CommentReport>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommentInfoService _publicationInfoService;

    public CommentReportProcessor(IUnitOfWork unitOfWork, ICommentInfoService publicationInfoService)
    {
        _unitOfWork = unitOfWork;
        _publicationInfoService = publicationInfoService;
    }

    public async Task ProcessReportAsync(CommentReport report, CancellationToken token)
    {
        var info = await RequestInfoBuilder.GetInfoAsync(report, _unitOfWork);
        await ProcessPublicationsAsync(report, info, token);
    }


    private async Task ProcessPublicationsAsync(CommentReport report, RequestInfo info, CancellationToken token)
    {
        int count = 0, i = report.Process;
        for (; i < report.Publications.Count; i++)
        {
            var publication = report.Publications.ElementAt(i);
            try
            {
                var result = await GetAsync(publication, info);
                report.SetComments(new CommentsDto(publication.ItemId, publication.OwnerId, result));
                count = 0;
            }
            catch (VkRequestException ex)
            {
                report.SetComments(new CommentsDto(publication.ItemId, publication.OwnerId));
                if (ex.Code != 212)
                {
                    count++;
                    if (count > 5) throw new TooManyRequestErrorsException(ex.Message);
                }
            }
            if (i % 60 == 0) await SaveAsync(report);
            await Task.Delay(1000, token);
        }

        await SaveAsync(report);
    }

    private async Task SaveAsync(CommentReport report)
    {
        await _unitOfWork.CommentReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private Task<List<(long, string)>> GetAsync(Publication publication, RequestInfo info) =>
        _publicationInfoService.GetAsync(info, publication.ItemId, publication.OwnerId, 500);
}