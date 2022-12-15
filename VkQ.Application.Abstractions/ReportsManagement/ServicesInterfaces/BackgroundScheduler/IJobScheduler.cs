namespace VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.BackgroundScheduler;

public interface IJobScheduler
{
    public Task<string> StartLikeReportAsync(Guid reportId, DateTimeOffset? startAt = null);
    public Task<string> StartParticipantReportAsync(Guid reportId, DateTimeOffset? startAt = null);
    public Task CancelAsync(string? jobId);
}