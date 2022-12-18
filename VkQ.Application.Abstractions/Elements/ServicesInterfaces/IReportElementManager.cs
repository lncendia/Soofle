using VkQ.Application.Abstractions.Elements.DTOs;
using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

namespace VkQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IReportElementManager
{

    public Task<List<LikeElementDto>> GetLikeReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query);

    public Task<List<ParticipantElementDto>> GetParticipantReportElementsAsync(Guid userId, Guid reportId,
        ParticipantElementSearchQuery query);
}