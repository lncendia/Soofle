using VkQ.Application.Abstractions.Elements.DTOs.LikeElementDto;
using VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.ViewModels.Elements;

namespace VkQ.WEB.Mappers.ElementMapper;

public class ElementMapper : IElementMapper
{
    public Lazy<IElementMapperUnit<LikeElementDto, LikeElementViewModel>> LikeElementMapper =>
        new(() => new LikeElementMapper());

    public Lazy<IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>> ParticipantElementMapper =>
        new(() => new ParticipantElementMapper());
}