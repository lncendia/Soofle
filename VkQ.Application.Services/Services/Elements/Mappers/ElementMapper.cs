using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Elements.Mappers;

public class ElementMapper : IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeReportElement>> LikeReportElementMapper =>
        new(() => new LikeReportElementMapper());

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper => new(() => new ParticipantReportElementMapper());
}