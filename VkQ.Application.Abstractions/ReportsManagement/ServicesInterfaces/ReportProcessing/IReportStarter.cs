namespace VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;

public interface IReportStarter
{
    Task StartLikeReportAsync(Guid id, CancellationToken token);
    Task StartParticipantReportAsync(Guid id, CancellationToken token);
}