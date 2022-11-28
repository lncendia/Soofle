namespace VkQ.Application.Abstractions.Interfaces.BackgroundScheduler;

public interface IJobStorage
{
    public Task<string> GetJobIdAsync(Guid reportId);
    public Task StoreJobIdAsync(Guid reportId, string jobId);
    public Task DeleteJobIdAsync(Guid reportId);
}