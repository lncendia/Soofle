using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.BackgroundScheduler;
using VkQ.Application.Abstractions.ReportsManagement.ServicesInterfaces.ReportProcessing;

namespace VkQ.Infrastructure.BackgroundTasks;

public class JobScheduler : IJobScheduler
{
    public Task<string> StartLikeReportAsync(Guid reportId, DateTimeOffset? startAt = null)
    {
        var id = startAt.HasValue
            ? Hangfire.BackgroundJob.Schedule<IReportStarter>(
                starter => starter.StartLikeReportAsync(reportId, CancellationToken.None), startAt.Value)
            : Hangfire.BackgroundJob.Enqueue<IReportStarter>(x =>
                x.StartLikeReportAsync(reportId, CancellationToken.None));

        return Task.FromResult(id);
    }

    public Task<string> StartParticipantReportAsync(Guid reportId, DateTimeOffset? startAt = null)
    {
        var id = startAt.HasValue
            ? Hangfire.BackgroundJob.Schedule<IReportStarter>(
                starter => starter.StartParticipantReportAsync(reportId, CancellationToken.None), startAt.Value)
            : Hangfire.BackgroundJob.Enqueue<IReportStarter>(x =>
                x.StartParticipantReportAsync(reportId, CancellationToken.None));

        return Task.FromResult(id);
    }

    public Task CancelAsync(string? jobId)
    {
        Hangfire.BackgroundJob.Delete(jobId);
        return Task.CompletedTask;
    }
}