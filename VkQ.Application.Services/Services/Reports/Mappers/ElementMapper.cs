using VkQ.Application.Abstractions.Elements.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.Elements.ServicesInterfaces;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.LikeReportDto;
using VkQ.Application.Abstractions.ReportsQuery.DTOs.ParticipantReportDto;
using VkQ.Application.Abstractions.ReportsQuery.ServicesInterfaces;
using VkQ.Domain.Reposts.LikeReport.Entities;
using VkQ.Domain.Reposts.ParticipantReport.Entities;

namespace VkQ.Application.Services.Services.Reports.Mappers;

public class ElementMapper : IElementMapper
{
    public Lazy<IReportMapperUnit<LikeReportDto, LikeReport>> LikeReportMapper => new(() => new LikeReportMapper());

    public Lazy<IReportMapperUnit<ParticipantReportDto, ParticipantReport>> ParticipantReportMapper =>
        new(() => new ParticipantReportMapper());

    public Lazy<IElementMapperUnit<LikeReportElementDto, LikeReportElement>> LikeReportElementMapper =>
        new(() => new LikeReportElementMapper());

    public Lazy<IElementMapperUnit<ParticipantReportElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper => new(() => new ParticipantReportElementMapper());
}