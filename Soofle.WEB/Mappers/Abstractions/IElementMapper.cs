using Soofle.Application.Abstractions.Elements.DTOs.CommentElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.LikeElementDto;
using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using Soofle.WEB.ViewModels.Elements;

namespace Soofle.WEB.Mappers.Abstractions;

public interface IElementMapper
{
    Lazy<IElementMapperUnit<LikeElementDto, LikeElementViewModel>> LikeElementMapper { get; }
    Lazy<IElementMapperUnit<CommentElementDto, CommentElementViewModel>> CommentElementMapper { get; }

    Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>> ParticipantElementMapper { get; }
}