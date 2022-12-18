using VkQ.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using VkQ.WEB.Mappers.Abstractions;
using VkQ.WEB.ViewModels.Elements;

namespace VkQ.WEB.Mappers.ElementMapper;

public class ParticipantElementMapper : IElementMapperUnit<ParticipantElementDto, ParticipantElementViewModel>
{
    public ParticipantElementViewModel Map(ParticipantElementDto element) => MapRecursion(element);

    private static ParticipantElementViewModel MapRecursion(ParticipantElementDto dto)
    {
        return new ParticipantElementViewModel(dto.Name, dto.VkId, dto.NewName, dto.Type);
    }
}