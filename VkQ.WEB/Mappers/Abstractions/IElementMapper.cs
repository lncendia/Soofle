using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using VkQ.WEB.ViewModels.Elements;

namespace VkQ.WEB.Mappers.Abstractions;

public interface IElementMapper
{
    Lazy<IElementMapperUnit<LikeElementDto, LikeElementViewModel>> LikeElementMapper { get; }

    Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>> ParticipantElementMapper { get; }
}