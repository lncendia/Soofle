using Soofle.Application.Abstractions.Elements.DTOs;
using Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.LikeElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;

namespace Soofle.Application.Abstractions.Elements.ServicesInterfaces;

public interface IReportElementManager
{
    public Task<LikeReportElementsDto> GetLikeReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query);

    public Task<CommentReportElementsDto> GetCommentReportElementsAsync(Guid userId, Guid reportId,
        PublicationElementSearchQuery query);

    public Task<List<ParticipantElementDto>> GetParticipantReportElementsAsync(Guid userId, Guid reportId,
        ParticipantElementSearchQuery query);
}