using Microsoft.EntityFrameworkCore;
using Soofle.Application.Abstractions.Jobs.ServicesInterfaces;
using Soofle.Infrastructure.ApplicationDataStorage.Models;

namespace Soofle.Infrastructure.ApplicationDataStorage;

public class JobStorage : IJobStorage
{
    private readonly ApplicationContext _context;

    public JobStorage(ApplicationContext context) => _context = context;

    public async Task<string?> GetJobIdAsync(Guid reportId)
    {
        return (await _context.Jobs.FirstOrDefaultAsync(r => r.ReportId == reportId))?.JobId;
    }

    public Task StoreJobIdAsync(Guid reportId, string jobId)
    {
        _context.Jobs.Add(new Job { ReportId = reportId, JobId = jobId });
        return _context.SaveChangesAsync();
    }

    public Task DeleteJobIdAsync(Guid reportId)
    {
        var job = _context.Jobs.FirstOrDefault(r => r.ReportId == reportId);
        if (job != null) _context.Jobs.Remove(job);
        return _context.SaveChangesAsync();
    }
}