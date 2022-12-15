using VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantReportDto;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapper
{
    public Lazy<IElementMapperUnit<LikeReportElementDto, LikeReportElement>> LikeReportElementMapper { get; }

    public Lazy<IElementMapperUnit<ParticipantReportElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper { get; }
}