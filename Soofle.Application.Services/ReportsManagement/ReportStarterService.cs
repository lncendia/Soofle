using Microsoft.Extensions.Logging;
using Soofle.Application.Abstractions.Proxies.Exceptions;
using Soofle.Application.Abstractions.Proxies.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsManagement.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsProcessors.Exceptions;
using Soofle.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using Soofle.Application.Abstractions.ReportsQuery.Exceptions;
using Soofle.Application.Abstractions.Users.Exceptions;
using Soofle.Application.Abstractions.VkRequests.Exceptions;
using Soofle.Domain.Abstractions.UnitOfWorks;
using Soofle.Domain.Reposts.BaseReport.Exceptions;
using Soofle.Domain.Reposts.PublicationReport.Exceptions;

namespace Soofle.Application.Services.ReportsManagement;

public class ReportStarterService : IReportStarter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportProcessorService _processorService;
    private readonly IReportInitializerService _initializerService;
    private readonly IProxyGetter _proxySetter;
    private readonly ILogger<ReportStarterService> _logger;

    public ReportStarterService(IUnitOfWork unitOfWork, IProxyGetter proxySetter,
        IReportInitializerService initializerService, IReportProcessorService processorService,
        ILogger<ReportStarterService> logger)
    {
        _unitOfWork = unitOfWork;
        _proxySetter = proxySetter;
        _initializerService = initializerService;
        _processorService = processorService;
        _logger = logger;
    }

    public async Task StartLikeReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            await CheckProxyAsync(report.UserId);
            if (!report.IsStarted)
                await _initializerService.LikeReportInitializer.Value.InitializeReportAsync(report, token);
            await _processorService.LikeReportProcessor.Value.ProcessReportAsync(report, token);
            report.Finish();
        }
        catch (Exception e)
        {
            report.Finish(HandleException(e));
        }

        await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task StartCommentReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.CommentReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            await CheckProxyAsync(report.UserId);
            if (!report.IsStarted)
                await _initializerService.CommentReportInitializer.Value.InitializeReportAsync(report, token);
            await _processorService.CommentReportProcessor.Value.ProcessReportAsync(report, token);
            report.Finish();
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "The report ended with an error. Id {ReportId}", report.Id);
            report.Finish(HandleException(e));
        }

        await _unitOfWork.CommentReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task StartParticipantReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            await CheckProxyAsync(report.UserId);
            if (!report.IsStarted)
                await _initializerService.ParticipantReportInitializer.Value.InitializeReportAsync(report, token);
            await _processorService.ParticipantReportProcessor.Value.ProcessReportAsync(report, token);
            report.Finish();
        }
        catch (Exception e)
        {
            report.Finish(HandleException(e));
        }

        await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private static string HandleException(Exception ex)
    {
        return ex switch
        {
            ReportAlreadyCompletedException => "Отчёт уже является сформированным",
            VkRequestException => $"Ошибка VK: {ex.Message}",
            UnableFindProxyException => "Не удалось найти подходящий прокси сервер",
            ReportNotStartedException => "Отчёт не инициализован",
            VkIsNotActiveException => "Ваш VK аккаунт не авторизован",
            ProxyIsNotSetException => "У вас не установлен прокси",
            TooManyRequestErrorsException => $"Не удается получить информацию. {ex.Message}",
            UserNotFoundException => "Создатель отчёта не найден",
            ElementsListEmptyException => "Участники не найдены",
            PublicationsListEmptyException => "Публикации по хештегу не найдены",
            HttpRequestException => "Возникла ошибка с подключением на сервере",
            _ => throw ex
        };
    }

    private async Task CheckProxyAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        if (user.Vk == null) throw new VkIsNotActiveException();
        if (!user.ProxyId.HasValue)
        {
            user.SetProxy(await _proxySetter.GetAsync());
            await _unitOfWork.UserRepository.Value.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}