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
            report.Finish(HandleException(e, id));
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
            report.Finish(HandleException(e, id));
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
            report.Finish(HandleException(e, id));
        }

        await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
        await _unitOfWork.SaveChangesAsync();
    }

    private string HandleException(Exception ex, Guid id)
    {
        switch (ex)
        {
            case VkIsNotActiveException:
                return "Ваш VK аккаунт не авторизован";
            case UnableFindProxyException: 
                return "Не удалось найти подходящий прокси сервер";
            case ProxyIsNotSetException: 
                return "У вас не установлен прокси";
            case ReportAlreadyCompletedException:
                return "Отчёт уже является сформированным";
            case VkRequestException:
                return $"Ошибка VK: {ex.Message}";
            case ReportNotStartedException:
                return "Отчёт не инициализован";
            case TooManyRequestErrorsException:
                return $"Не удается получить информацию. {ex.Message}";
            case UserNotFoundException:
                return "Создатель отчёта не найден";
            case ElementsListEmptyException:
                return "Участники не найдены";
            case PublicationsListEmptyException:
                return "Публикации по хештегу не найдены";
            case HttpRequestException httpEx:
                if (httpEx.InnerException is OperationCanceledException) throw httpEx.InnerException;
                _logger.LogError(ex.InnerException, "Report {Id} completed with an error", id);
                return "Возникла ошибка с подключением на сервере";
            default:
                if (ex is not OperationCanceledException)
                    _logger.LogError(ex, "Report {Id} completed with an error", id);
                throw ex;
        }
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