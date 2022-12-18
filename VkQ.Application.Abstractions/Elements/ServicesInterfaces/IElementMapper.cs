using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeReportElement>> LikeReportElementMapper { get; }

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper { get; }
}