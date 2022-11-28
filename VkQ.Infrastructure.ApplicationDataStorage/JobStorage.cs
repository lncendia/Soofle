using VkQ.Application.Abstractions.Interfaces.BackgroundScheduler;

namespace VkQ.Infrastructure.ApplicationDataStorage;

public class JobStorage:IJobStorage
{
    public Task<string> GetJobIdAsync(Guid reportId)
    {
        throw new NotImplementedException();
    }

    public Task StoreJobIdAsync(Guid reportId, string jobId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteJobIdAsync(Guid reportId)
    {
        throw new NotImplementedException();
    }
}