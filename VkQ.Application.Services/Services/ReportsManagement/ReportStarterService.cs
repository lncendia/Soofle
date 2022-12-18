using VkQ.Application.Abstractions.Proxies.Exceptions;
using VkQ.Application.Abstractions.Proxies.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;
using VkQ.Application.Abstractions.ReportsProcessors.Exceptions;
using VkQ.Application.Abstractions.ReportsProcessors.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.Exceptions;
using VkQ.Application.Abstractions.Users.Exceptions.UsersAuthentication;
using VkQ.Domain.Abstractions.UnitOfWorks;
using VkQ.Domain.Reposts.BaseReport.Exceptions.Base;

namespace VkQ.Application.Services.Services.ReportsManagement;

public class ReportStarterService : IReportStarter
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportProcessorService _processorService;
    private readonly IReportInitializerService _initializerService;
    private readonly IProxyGetter _proxySetter;

    public ReportStarterService(IUnitOfWork unitOfWork, IProxyGetter proxySetter,
        IReportInitializerService initializerService, IReportProcessorService processorService)
    {
        _unitOfWork = unitOfWork;
        _proxySetter = proxySetter;
        _initializerService = initializerService;
        _processorService = processorService;
    }

    public async Task StartLikeReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.LikeReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            if (!report.IsStarted)
            {
                await _initializerService.LikeReportInitializer.Value.InitializeReportAsync(report, token);
                await _unitOfWork.LikeReportRepository.Value.UpdateAsync(report);
                await _unitOfWork.SaveChangesAsync();
            }

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

    public async Task StartParticipantReportAsync(Guid id, CancellationToken token)
    {
        var report = await _unitOfWork.ParticipantReportRepository.Value.GetAsync(id);
        if (report == null) throw new ReportNotFoundException();
        try
        {
            await CheckProxyAsync(report.UserId);
            if (!report.IsStarted)
            {
                await _initializerService.ParticipantReportInitializer.Value.InitializeReportAsync(report, token);
                await _unitOfWork.ParticipantReportRepository.Value.UpdateAsync(report);
                await _unitOfWork.SaveChangesAsync();
            }

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
            ReportAlreadyCompletedException => "Отчёт уже является сформированным.",
            UnableFindProxyException => "Не удалось найти подходящий прокси сервер.",
            ReportNotStartedException => "Отчёт не инициализован.",
            VkIsNotActiveException => "Ваш VK аккаунт не авторизован.",
            ProxyIsNotSetException => "У вас не установлен прокси.",
            TooManyRequestErrorsException => $"Ошибка VK: {ex.Message}.",
            UserNotFoundException => "Создатель отчёта не найден.",
            _ => throw ex
        };
    }

    private async Task CheckProxyAsync(Guid userId)
    {
        var user = await _unitOfWork.UserRepository.Value.GetAsync(userId);
        if (user == null) throw new UserNotFoundException();
        if (user.Vk == null) throw new VkIsNotActiveException();
        if (!user.Vk.ProxyId.HasValue) await _proxySetter.GetAsync();
    }
}