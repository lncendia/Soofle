namespace Soofle.Application.Abstractions.BackgroundJobs.ServicesInterfaces;

public interface IReportsRemovingService
{
    Task CheckAndRemoveAsync();
}