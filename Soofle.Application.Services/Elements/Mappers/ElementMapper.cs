using Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.LikeElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using Soofle.Application.Abstractions.Elements.ServicesInterfaces;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.Elements.Mappers;

public class ElementMapper : IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeReportElement>> LikeReportElementMapper =>
        new(() => new LikeReportElementMapper());

    public Lazy<IElementMapperUnit<CommentElementDto, CommentReportElement>> CommentReportElementMapper =>
        new(() => new CommentReportElementMapper());

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper => new(() => new ParticipantReportElementMapper());
}