using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using Soofle.WEB.Mappers.Abstractions;
using Soofle.WEB.ViewModels.Elements;

namespace Soofle.WEB.Mappers.ElementMapper;

public class ParticipantElementMapper : IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>
{
    public ParticipantElementViewModel Map(ParticipantElementDto element) => MapRecursion(element);

    private static ParticipantElementViewModel MapRecursion(ParticipantElementDto dto) =>
        new(dto.Name, dto.VkId, dto.NewName, dto.Type, dto.ParticipantType, dto.Children.Select(MapRecursion));
}