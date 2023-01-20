using Soofle.Application.Abstractions.Elements.DTOs.ParticipantElementDto;
using Soofle.Application.Abstractions.Elements.ServicesInterfaces;
using Soofle.Domain.Reposts.ParticipantReport.Entities;

namespace Soofle.Application.Services.Elements.Mappers;

public class ParticipantReportElementMapper : IElementMapperUnit<ParticipantElementDto, ParticipantReportElement>
{
    public List<ParticipantElementDto> Map(IEnumerable<ParticipantReportElement> elements)
    {
        var groupedElements = elements.GroupBy(x => x.Parent).ToList();
        if (!groupedElements.Any()) return new List<ParticipantElementDto>();
        return (from element in groupedElements.First(x => x.Key == null)
            let children = groupedElements.FirstOrDefault(x => x.Key == element)?.Select(x => Map(x, null))
            select Map(element, children)).ToList();
    }

    private static ParticipantElementDto Map(ParticipantReportElement element,
        IEnumerable<ParticipantElementDto>? children)
    {
        var elementBuilder = ParticipantElementBuilder.ParticipantReportElementDto();
        elementBuilder
            .WithParticipantType(element.ParticipantType)
            .WithName(element.Name)
            .WithVkId(element.VkId);

        if (element.Type.HasValue) elementBuilder.WithType(element.Type.Value, element.NewName);
        if (children != null) elementBuilder.WithChildren(children);
        return elementBuilder.Build();
    }
}