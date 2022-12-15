using VkQ.Application.Abstractions.Elements.DTOs;
using VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantReportDto;

namespace VkQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IReportElementManager
{

    public Task<List<LikeReportElementDto>> GetLikeReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query);

    public Task<List<ParticipantReportElementDto>> GetParticipantReportElementsAsync(Guid userId, Guid reportId,
        ParticipantElementSearchQuery query);
}