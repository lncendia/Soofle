using Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.LikeElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using Soofle.Domain.Reposts.CommentReport.Entities;
using Soofle.Domain.Reposts.LikeReport.Entities;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Abstractions.Elements.ServicesInterfaces;

public interface IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeReportElement>> LikeReportElementMapper { get; }
    public Lazy<IElementMapperUnit<CommentElementDto, CommentReportElement>> CommentReportElementMapper { get; }

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>>
        ParticipantReportElementMapper { get; }
}