namespace VkQ.Application.Abstractions.Reports.ServicesInterfaces;

public interface IReportStarter
{
    Task StartLikeReportAsync(Guid id, CancellationToken token);
    Task StartParticipantReportAsync(Guid id, CancellationToken token);
}