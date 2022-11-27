namespace VkQ.Application.Abstractions.Interfaces.BackgroundScheduler;

public interface IJobScheduler
{
    public Task<string> StartLikeReportAsync(Guid reportId);
    public Task<string> StartParticipantReportAsync(Guid reportId);
    public Task CancelAsync(string jobId);
}