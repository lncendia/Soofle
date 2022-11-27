namespace VkQ.Application.Abstractions.Interfaces.BackgroundScheduler;

public interface IJobManager
{
    public Task StartLikeReportAsync(Guid reportId);
    public Task StartParticipantReportAsync(Guid reportId);
    public Task CancelLikeReportAsync(Guid reportId);
    public Task CancelParticipantReportAsync(Guid reportId);
}