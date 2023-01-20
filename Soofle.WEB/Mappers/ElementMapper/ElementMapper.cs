using Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.LikeElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Elements;

namespace Soofle.WEB.Mappers.ElementMapper;

public class ElementMapper : IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeElementViewModel>> LikeElementMapper =>
        new(() => new LikeElementMapper());

    public Lazy<IElementMapperUnit<CommentElementDto, CommentElementViewModel>> CommentElementMapper =>
        new(() => new CommentElementMapper());

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>> ParticipantElementMapper =>
        new(() => new ParticipantElementMapper());
}