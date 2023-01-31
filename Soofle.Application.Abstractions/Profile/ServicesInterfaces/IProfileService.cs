using Soofle.Application.Abstractions.Profile.DTOs;

namespace Soofle.Application.Abstractions.Profile.ServicesInterfaces;

public interface IProfileService
{
    Task<ProfileDto> GetAsync(Guid userId);
    Task<StatsDto> GetStatisticAsync(Guid userId);

    Task<List<LinkDto>> GetLinksAsync(Guid userId);

    Task<List<PaymentDto>> GetPaymentsAsync(Guid userId);
}